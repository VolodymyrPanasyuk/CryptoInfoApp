using System;

namespace CryptoInfoApp.Model
{
    public class HomeModel
    {
        public string Id { get; set; }
        public long? Rank { get; set; }
        public Uri? Image { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceChangePercentage24H { get; set; }
        public decimal? PriceChangePercentage7D { get; set; }
        public decimal? MarketCap { get; set; }
        public decimal? Volume24H { get; set; }
        public decimal? CirculatingSupply { get; set; }
    }
}