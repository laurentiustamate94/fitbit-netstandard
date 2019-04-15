using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Fitbit.Api.Abstractions.Models.HeartRate
{
    public class HeartActivitiesIntraDayDataset
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
