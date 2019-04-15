using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Fitbit.Api.Abstractions.Models
{
    public class ResponseBase
    {
        [JsonProperty("errors")]
        public IEnumerable<ErrorModel> Errors { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; } = true;
    }
}
