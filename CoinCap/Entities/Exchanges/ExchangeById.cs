using System.Text.Json.Serialization;

namespace CoinCap.Entities.Exchanges
{
    public class ExchangeById
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

        [JsonPropertyName("percentTotalVolume")]
        public decimal? PercentTotalVolume { get; set; }

        [JsonPropertyName("volumeUsd")]
        public decimal? VolumeUsd { get; set; }

        [JsonPropertyName("tradingPairs")]
        public int? TradingPairs { get; set; }

        [JsonPropertyName("socket")]
        public bool? Socket { get; set; }

        [JsonPropertyName("exchangeUrl")]
        public Uri? ExchangeUrl { get; set; }

        [JsonPropertyName("updated")]
        public long? Updated { get; set; }
    }
}