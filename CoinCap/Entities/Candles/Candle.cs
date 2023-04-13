using System.Text.Json.Serialization;

namespace CoinCap.Entities.Candles
{
    public class Candle
    {
        [JsonPropertyName("open")]
        public string? Open { get; set; }

        [JsonPropertyName("high")]
        public string? High { get; set; }

        [JsonPropertyName("low")]
        public string? Low { get; set; }

        [JsonPropertyName("close")]
        public string? Close { get; set; }

        [JsonPropertyName("volume")]
        public string? Volume { get; set; }

        [JsonPropertyName("period")]
        public long? Period { get; set; }
    }
}