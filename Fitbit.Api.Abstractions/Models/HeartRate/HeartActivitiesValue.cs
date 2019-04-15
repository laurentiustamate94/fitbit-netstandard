using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Fitbit.Api.Abstractions.Models.HeartRate
{
    public class HeartActivitiesValue
    {
        [JsonProperty("customHeartRateZones")]
        public List<HeartRateZone> CustomHeartRateZones { get; set; }

        [JsonProperty("heartRateZones")]
        public List<HeartRateZone> HeartRateZones { get; set; }

        [JsonProperty("restingHeartRate")]
        public int RestingHeartRate { get; set; }
    }
}
