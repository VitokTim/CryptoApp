using System.Text.Json.Serialization;

namespace CryptoApp.Models
{
    public class Crypto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("priceUsd")]
        public string PriceUsd { get; set; }

        [JsonPropertyName("volumeUsd24Hr")]
        public string VolumeUsd24Hr { get; set; }

        [JsonPropertyName("changePercent24Hr")]
        public string ChangePercent24Hr { get; set; }

        [JsonPropertyName("explorer")]
        public string Explorer { get; set; }
    }
}
