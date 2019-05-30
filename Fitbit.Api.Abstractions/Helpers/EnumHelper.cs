using System;
using System.Collections.Generic;
using System.Text;

namespace Fitbit.Api.Abstractions.Helpers
{
    public static class EnumHelper
    {
        public static string ToUrlParameter(this PeriodType period)
        {
            switch (period)
            {
                case PeriodType.OneDay:
                    return "1d";
                case PeriodType.SevenDays:
                    return "7d";
                case PeriodType.ThirtyDays:
                    return "30d";
                case PeriodType.OneWeek:
                    return "1w";
                case PeriodType.OneMonth:
                    return "1m";
                default:
                    throw new Exception($"Unrecognized {typeof(PeriodType)} - '{period}'");
            }
        }
    }
}
