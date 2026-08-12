using System.Text.Json.Serialization;

namespace Majors
{
    public record Major
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("direction")]
        public string Name { get; set; } = string.Empty;
        public string Campaign { get; set; } = string.Empty;
        // Бюджетные места без квоты
        [JsonPropertyName("places")]
        public int? CurrentPlaces { get; set; }
        // Бюджетные места с учетом квоты
        [JsonPropertyName("budget_places")]
        public int? BudgetPlaces { get; set; }
        [JsonIgnore]
        public int Places => CurrentPlaces ?? BudgetPlaces ?? 0;
    }
}