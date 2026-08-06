using System.Text.Json;
using Majors;

namespace Settings
{
    public class SettingsService
    {
        private const string FileName = "config.json";

        public async Task<SettingsConfiguration> LoadAsync()
        {
            if (!File.Exists(FileName))
                return new SettingsConfiguration();

            var json = await File.ReadAllTextAsync(FileName);

            return JsonSerializer.Deserialize<SettingsConfiguration>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new SettingsConfiguration();
        }

        public async Task SaveAsync(SettingsConfiguration settingsConfiguration)
        {
            var json = JsonSerializer.Serialize(settingsConfiguration,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            await File.WriteAllTextAsync(FileName, json);
        }
    }
}