using System;
using System.Collections.Generic;
using System.Text;

namespace Fitbit.Api.Abstractions.Models.Authentication
{
    public enum ExpiryType
    {
        OneHour = 3600,
        EightHours = 28800,
        OneDay = 86400,
        OneWeek = 604800,
        ThirtyDays = 2592000,
        OneYear = 31536000
    }
}
