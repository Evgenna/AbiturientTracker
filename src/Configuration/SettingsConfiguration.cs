using Majors;

namespace Settings
{
    /// <summary>
    /// Конфигурация пользователя программы
    /// </summary>
    public class SettingsConfiguration
    {
        /// <summary>
        /// Идентификатор абитуриента
        /// </summary>
        public string Uid {get;set;} = string.Empty;
        /// <summary>
        /// Список выбранных специальностей
        /// </summary>
        public List<Major> Majors {get;set;} = [];
    }
}