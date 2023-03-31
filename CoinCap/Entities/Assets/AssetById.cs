using System.Text.Json.Serialization;

namespace CoinCap.Entities.Assets
{
    public class AssetById
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("supply")]
        public decimal? Supply { get; set; }

        [JsonPropertyName("maxSupply")]
        public decimal? MaxSupply { get; set; }

        [JsonPropertyName("marketCapUsd")]
        public decimal? MarketCapUsd { get; set; }

        [JsonPropertyName("volumeUsd24Hr")]
        public decimal? VolumeUsd24Hr { get; set; }

        [JsonPropertyName("priceUsd")]
        public decimal? PriceUsd { get; set; }

        [JsonPropertyName("changePercent24Hr")]
        public decimal? ChangePercent24Hr { get; set; }

        [JsonPropertyName("vwap24Hr")]
        public decimal? Vwap24Hr { get; set; }
    }
}