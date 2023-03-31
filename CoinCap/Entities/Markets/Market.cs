using System.Text.Json.Serialization;

namespace CoinCap.Entities.Markets
{
    public class Market
    {
        [JsonPropertyName("exchangeId")]
        public string ExchangeId { get; set; }

        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

        [JsonPropertyName("baseSymbol")]
        public string BaseSymbol { get; set; }

        [JsonPropertyName("baseId")]
        public string BaseId { get; set; }

        [JsonPropertyName("quoteSymbol")]
        public string QuoteSymbol { get; set; }

        [JsonPropertyName("quoteId")]
        public string QuoteId { get; set; }

        [JsonPropertyName("priceQuote")]
        public double? PriceQuote { get; set; }

        [JsonPropertyName("priceUsd")]
        public double? PriceUsd { get; set; }

        [JsonPropertyName("volumeUsd24Hr")]
        public double? VolumeUsd24Hr { get; set; }

        [JsonPropertyName("percentExchangeVolume")]
        public double? PercentExchangeVolume { get; set; }

        [JsonPropertyName("tradesCount24Hr")]
        public int? TradesCount24Hr { get; set; }

        [JsonPropertyName("updated")]
        public long? Updated { get; set; }
    }
}