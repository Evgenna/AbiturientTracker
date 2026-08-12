using Abiturients;
using Majors;
using Settings;
using University;

namespace Statistics
{
    public class StatisticsService(
        UniversityProxy universityProxy,
        SettingsService settingsService
        )
    {
        private UniversityProxy _universityProxy = universityProxy;
        private SettingsService _settingsService = settingsService;

        public async Task<MyStatistics> GetMyStatistics(List<Abiturient> _abiturients)
        {
            var myData = await _settingsService.LoadAsync();
            string myId = myData.Uid;

            int place = _abiturients.FindIndex(a => a.Uid == myId);
            string currentMajor = "";
            string agreementMajor = "";
            int withAgreement = _abiturients.Slice(0, place).Count(a => a.HasAgreement);
            int withoutAgreement = _abiturients.Slice(0, place).Count(a => !a.HasAgreement);

            return new MyStatistics(
                place,
                currentMajor,
                agreementMajor,
                withAgreement,
                withAgreement
            );
        }

        public async Task<List<MajorStatistics>> GetMajorStatistics(List<MajorDetails> _majors, List<Abiturient> _abiturients)
        {
            var majorStatistics = new List<MajorStatistics>();

            foreach (MajorDetails major in _majors)
            {
                
                var majorInfo = new MajorStatistics(
                    major,
                    major.Places,
                    1,
                    1,
                    1,
                    1,
                    1
                );

                majorStatistics.Add(majorInfo);
            }

            return majorStatistics;
        }

        public async Task<StatisticsResponse> GetStatistics()
        {
            var _data = await _universityProxy.GetAbiturients();
            var _abiturients = _data.Abiturients;
            var _majors = _data.Majors;

            int totalCount = _abiturients.Count;
            int agreementCount = _abiturients.Count(a => a.HasAgreement);

            MyStatistics myStatistics = await GetMyStatistics(_abiturients);

            List<MajorStatistics> majorStatistics = await GetMajorStatistics(_majors, _abiturients);

            return new StatisticsResponse(
                totalCount, 
                agreementCount,
                myStatistics,
                majorStatistics
            );
        }
    }
}