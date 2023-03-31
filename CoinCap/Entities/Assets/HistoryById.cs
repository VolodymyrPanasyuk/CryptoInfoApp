using System.Text.Json.Serialization;

namespace CoinCap.Entities.Assets
{
    public class HistoryById
    {
        [JsonPropertyName("priceUsd")]
        public decimal? PriceUsd { get; set; }

        [JsonPropertyName("time")]
        public long? Time { get; set; }
    }
}