using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Fitbit.Api.Abstractions.Models.HeartRate
{
    public class HeartRateTimeSeries : ResponseBase
    {
        [JsonProperty("activities-heart")]
        public List<HeartActivities> HeartActivities { get; set; }

        [JsonProperty("activities-heart-intraday")]
        public HeartActivitiesIntraDay HeartActivitiesIntraDay { get; set; }
    }
}
