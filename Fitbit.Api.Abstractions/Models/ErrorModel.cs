using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Fitbit.Api.Abstractions.Models
{
    public class ErrorModel
    {
        [JsonProperty("errorType")]
        public string ErrorType { get; set; }

        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
