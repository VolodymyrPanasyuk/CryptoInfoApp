using System.Text.Json.Serialization;

namespace CoinCap.Entities.Assets
{
    public class HistoryById
    {
        [JsonPropertyName("priceUsd")]
        public double? PriceUsd { get; set; }

        [JsonPropertyName("time")]
        public long? Time { get; set; }
    }
}