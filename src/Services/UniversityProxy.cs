using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using Majors;
using Settings;
using Abiturients;

namespace University
{
    public class UniversityProxy(
        IHttpClientFactory httpClientFactory,
        IMemoryCache memoryCache,
        IOptions<UniversityConfiguration> universityOptions,
        IOptions<SettingsConfiguration> abiturientOptions)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IMemoryCache _memoryCache = memoryCache;

        private readonly UniversityConfiguration _universityConfiguration = universityOptions.Value;
        private readonly SettingsConfiguration _settingsConfiguration = abiturientOptions.Value;

        private async Task<JsonDocument> SendRequest(string link, string? testPath = null)
        {
            string json;

            if (_universityConfiguration.UseTestData)
            {
                Console.WriteLine("Запрос из тестового окружения");
                var path = Path.Combine(
                    AppContext.BaseDirectory,
                    _universityConfiguration.TestDataPath,
                    testPath!
                );
                json = await File.ReadAllTextAsync(path);
            }
            else
            {
                Console.WriteLine("Отправлен внешний запрос");
                json = (await _memoryCache.GetOrCreateAsync(link, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

                    var client = _httpClientFactory.CreateClient();
                    return await client.GetStringAsync(link);
                }))!;
            }
            if(json is null)
            {
                throw new InvalidOperationException("Failed to retrieve data.");
            }

            return JsonDocument.Parse(json);
        }

        public async Task<List<MajorSummary>> GetMajors()
        {
            List<MajorSummary> majorList = new List<MajorSummary> { };
            foreach (string campaign in _universityConfiguration.Campaigns)
            {
                using var json = await SendRequest($"{_universityConfiguration.BaseUrl}/{campaign}",
                    $"{campaign}.json");

                var root = json.RootElement;

                var majors = root.GetProperty("contest_groups").Deserialize<List<MajorSummary>>();

                if (majors == null) continue;

                majorList.AddRange(
                    majors.Select(m => m with { Campaign = campaign })
                );
            }

            return majorList;
        }

        public async Task<List<UniversityData>> GetAbiturients()
        {
            var abiturientList = new List<AbiturientResponse>();
            var universityData = new List<UniversityData>();

            foreach (var major in _settingsConfiguration.Majors)
            {
                using var json = await SendRequest(
                    $"{_universityConfiguration.BaseUrl}/{major.Campaign}/contest_groups/{major.Id}",
                    $"{major.Campaign}_{major.Id}.json");

                var contestGroup = json.RootElement.GetProperty("contest_group");

                var majorDetail = contestGroup.Deserialize<MajorDetails>();

                var abiturients = contestGroup.GetProperty("abiturients").Deserialize<List<AbiturientResponse>>();
                if(abiturients == null) continue;

                abiturientList.AddRange(abiturients);

                universityData.Add(new UniversityData(
                    majorDetail! with
                    {
                        Campaign = major.Campaign
                    },
                    abiturientList.OrderByDescending(a => a.Rating).ToList()
                ));
            }
            return universityData;
        }
    }
}