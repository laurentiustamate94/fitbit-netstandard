using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Fitbit.Api.Abstractions.Models.HeartRate
{
    public class HeartRateZone
    {
        [JsonProperty("caloriesOut")]
        public decimal CaloriesOut { get; set; }

        [JsonProperty("max")]
        public int Max { get; set; }

        [JsonProperty("min")]
        public int Min { get; set; }

        [JsonProperty("minutes")]
        public int Minutes { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
