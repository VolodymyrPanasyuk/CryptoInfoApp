using System.Text.Json.Serialization;

namespace CoinCap.Entities.Assets
{
    public class MarketById
    {
        [JsonPropertyName("exchangeId")]
        public string ExchangeId { get; set; }

        [JsonPropertyName("baseId")]
        public string BaseId { get; set; }

        [JsonPropertyName("quoteId")]
        public string QuoteId { get; set; }

        [JsonPropertyName("baseSymbol")]
        public string BaseSymbol { get; set; }

        [JsonPropertyName("quoteSymbol")]
        public string QuoteSymbol { get; set; }

        [JsonPropertyName("volumeUsd24Hr")]
        public decimal? VolumeUsd24Hr { get; set; }

        [JsonPropertyName("priceUsd")]
        public decimal? PriceUsd { get; set; }

        [JsonPropertyName("volumePercent")]
        public decimal? VolumePercent { get; set; }
    }
}