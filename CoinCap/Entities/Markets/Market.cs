using System.Text.Json.Serialization;

namespace CoinCap.Entities.Markets
{
    public class Market
    {
        [JsonPropertyName("exchangeId")]
        public string ExchangeId { get; set; }

        [JsonPropertyName("rank")]
        public string? Rank { get; set; }

        [JsonPropertyName("baseSymbol")]
        public string BaseSymbol { get; set; }

        [JsonPropertyName("baseId")]
        public string BaseId { get; set; }

        [JsonPropertyName("quoteSymbol")]
        public string QuoteSymbol { get; set; }

        [JsonPropertyName("quoteId")]
        public string QuoteId { get; set; }

        [JsonPropertyName("priceQuote")]
        public string? PriceQuote { get; set; }

        [JsonPropertyName("priceUsd")]
        public string? PriceUsd { get; set; }

        [JsonPropertyName("volumeUsd24Hr")]
        public string? VolumeUsd24Hr { get; set; }

        [JsonPropertyName("percentExchangeVolume")]
        public string? PercentExchangeVolume { get; set; }

        [JsonPropertyName("tradesCount24Hr")]
        public string? TradesCount24Hr { get; set; }

        [JsonPropertyName("updated")]
        public long? Updated { get; set; }
    }
}