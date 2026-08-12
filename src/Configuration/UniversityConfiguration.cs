namespace University
{
    /// <summary>
    /// Конфигурация об университете
    /// </summary>
    public class UniversityConfiguration
    {
        /// <summary>
        /// Публичный API университета
        /// </summary>
        public string BaseUrl { get; set; } = string.Empty;
        /// <summary>
        /// Список уровней образования
        /// </summary>
        public List<string> Campaigns { get; set; } = [];
        public bool UseTestData { get; set; }
        /// <summary>
        /// Директория тестовых данных
        /// </summary>
        public string TestDataPath { get; set; } = "TestData";
    }
}