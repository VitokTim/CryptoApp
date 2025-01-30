using System.Text.Json.Serialization;

namespace CryptoApp.Models
{
    public class Crypto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("priceUsd")]
        public string PriceUsd { get; set; }
    }
}
