using System;
using System.Collections.Generic;

namespace CryptoInfoApp.Model
{
    public class CoinModel : HomeModel
    {
        public List<string>? Description { get; set; }
        public decimal? PriceChange24H { get; set; }
        public decimal? High24H { get; set; }
        public decimal? Low24H { get; set; }
        public decimal? FullyDilutedMarketCap { get; set; }
        public decimal? Ath { get; set; }
        public decimal? AthChangePercentage { get; set; }
        public DateTimeOffset? AthDate { get; set; }
        public decimal? Atl { get; set; }
        public decimal? AtlChangePercentage { get; set; }
        public DateTimeOffset? AtlDate { get; set; }
    }
}