using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CryptoApp.Models
{
    public class CryptoListResponse
    {
        [JsonPropertyName("data")]
        public List<Crypto> Data { get; set; }
    }
}
