using System.Text.Json;
using Majors;

namespace Settings
{
    /// <summary>
    /// Сервис для загрузки и сохранения пользоватльской конфигурации
    /// </summary>
    public class SettingsService
    {
        private const string FileName = "config.json";

        /// <summary>
        /// Загружает пользовательскую конфигурацию из файла
        /// </summary>
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

        /// <summary>
        /// Сохраняет пользовательскую конфигурацию в файл
        /// </summary>
        /// <param name="settingsConfiguration">Конфигурация для сохранения</param>
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