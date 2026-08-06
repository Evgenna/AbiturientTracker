namespace University
{
    public class UniversityConfiguration
    {
        public string BaseUrl { get; set; } = string.Empty;
        public List<string> Campaigns { get; set; } = [];
        public bool UseTestData { get; set; }
        public string TestDataPath { get; set; } = "TestData";
    }
}