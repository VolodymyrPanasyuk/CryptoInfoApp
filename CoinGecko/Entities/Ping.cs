using System.Text.Json.Serialization;

namespace CoinGecko.Entities
{
    public class Ping
    {
        [JsonPropertyName("gecko_says")]
        public string GeckoSays { get; set; }
    }
}