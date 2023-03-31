using System.Text.Json.Serialization;

namespace CoinCap.Entities.Rates
{
    public class RateById
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("currencySymbol")]
        public string CurrencySymbol { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("rateUsd")]
        public double? RateUsd { get; set; }
    }
}