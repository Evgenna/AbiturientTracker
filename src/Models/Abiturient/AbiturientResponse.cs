using System.Text.Json.Serialization;

using Majors;

namespace Abiturients
{
    /// <summary>
    /// Обработчик ответа с данными об абитуриенте
    /// </summary>
    public class AbiturientResponse
    {
        [JsonPropertyName("sspvo_unique_code")]
        public string Uid { get; set; } = string.Empty;
        [JsonPropertyName("rating")]
        public int Rating { get; set; }
        [JsonPropertyName("has_agreement")]
        public bool HasAgreement { get; set; }
        [JsonPropertyName("priority")]
        public int Priority { get; set; }
    }
}