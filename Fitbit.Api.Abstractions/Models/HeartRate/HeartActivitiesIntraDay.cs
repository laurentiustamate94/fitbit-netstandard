using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Fitbit.Api.Abstractions.Models.HeartRate
{
    public class HeartActivitiesIntraDay
    {
        [JsonProperty("dataset")]
        public List<HeartActivitiesIntraDayDataset> Dataset { get; set; }

        [JsonProperty("datasetInterval")]
        public int DatasetInterval { get; set; }

        [JsonProperty("datasetType")]
        public string DatasetType { get; set; }
    }
}
