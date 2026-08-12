using System.Text.Json.Serialization;

namespace Majors
{
    public record MajorDetails
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("direction")]
        public string Name { get; set; } = string.Empty;
        public string Campaign { get; set; } = string.Empty;
        [JsonPropertyName("places")]
        public int Places { get; set; }
    }
}