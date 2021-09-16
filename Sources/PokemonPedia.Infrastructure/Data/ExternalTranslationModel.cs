using System.Text.Json.Serialization;

namespace PokemonPedia.Infrastructure.Data
{
    public class Success
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

    public class Contents
    {
        [JsonPropertyName("translated")]
        public string Translated { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("translation")]
        public string Translation { get; set; }
    }

    public class Error
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class ExternalTranslationModel
    {
        [JsonPropertyName("success")]
        public Success Success { get; set; }

        [JsonPropertyName("contents")]
        public Contents Contents { get; set; }

        [JsonPropertyName("error")]
        public Error Error { get; set; }
    }

}
