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
        public string? Rank { get; set; }

        [JsonPropertyName("percentTotalVolume")]
        public string? PercentTotalVolume { get; set; }

        [JsonPropertyName("volumeUsd")]
        public string? VolumeUsd { get; set; }

        [JsonPropertyName("tradingPairs")]
        public string? TradingPairs { get; set; }

        [JsonPropertyName("socket")]
        public bool? Socket { get; set; }

        [JsonPropertyName("exchangeUrl")]
        public string? ExchangeUrl { get; set; }

        [JsonPropertyName("updated")]
        public long? Updated { get; set; }
    }
}