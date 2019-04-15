using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Fitbit.Api.Abstractions.Models.HeartRate
{
    public class HeartActivities
    {
        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("value")]
        public HeartActivitiesValue Value { get; set; }
    }
}
