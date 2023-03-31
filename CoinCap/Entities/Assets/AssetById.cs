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
        public double? Supply { get; set; }

        [JsonPropertyName("maxSupply")]
        public double? MaxSupply { get; set; }

        [JsonPropertyName("marketCapUsd")]
        public double? MarketCapUsd { get; set; }

        [JsonPropertyName("volumeUsd24Hr")]
        public double? VolumeUsd24Hr { get; set; }

        [JsonPropertyName("priceUsd")]
        public double? PriceUsd { get; set; }

        [JsonPropertyName("changePercent24Hr")]
        public double? ChangePercent24Hr { get; set; }

        [JsonPropertyName("vwap24Hr")]
        public double? Vwap24Hr { get; set; }
    }
}