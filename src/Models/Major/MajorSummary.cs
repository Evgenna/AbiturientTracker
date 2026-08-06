using System.Text.Json.Serialization;

namespace Majors
{
    public record MajorSummary
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("direction")]
        public string Name { get; set; } = string.Empty;
        public string Campaign { get; set; } = string.Empty;
        [JsonPropertyName("budget_places")]
        public int Places { get; set; }
    }
}