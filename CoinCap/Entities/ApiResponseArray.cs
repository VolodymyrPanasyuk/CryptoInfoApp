﻿using System.Text.Json.Serialization;

namespace CoinCap.Entities
{
    public class ApiResponseArray<T>
    {
        [JsonPropertyName("data")]
        public T[] Data { get; set; }

        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }
    }
}