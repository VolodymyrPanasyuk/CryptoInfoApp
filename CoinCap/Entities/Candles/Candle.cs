using System.Text.Json.Serialization;

namespace CoinCap.Entities.Candles
{
    public class Candle
    {
        [JsonPropertyName("open")]
        public double? Open { get; set; }

        [JsonPropertyName("high")]
        public double? High { get; set; }

        [JsonPropertyName("low")]
        public double? Low { get; set; }

        [JsonPropertyName("close")]
        public double? Close { get; set; }

        [JsonPropertyName("volume")]
        public double? Volume { get; set; }

        [JsonPropertyName("period")]
        public long? Period { get; set; }
    }
}