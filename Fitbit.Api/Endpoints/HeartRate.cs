using Fitbit.Api.Abstractions;
using Fitbit.Api.Abstractions.Endpoints;
using Fitbit.Api.Abstractions.Helpers;
using Fitbit.Api.Abstractions.Models.HeartRate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fitbit.Api.Endpoints
{
    public class HeartRate : EndpointBase, IHeartRate
    {
        public HeartRate(IAuthentication authentication)
            : base(authentication)
        {

        }

        public Task<object> GetHeartRateIntradayTimeSeriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<object> GetHeartRateTimeSeriesAsync(DateTime date, PeriodType period, string userId = Defaults.CurrentUser)
        {
            throw new NotImplementedException();
        }

        public Task<HeartRateTimeSeries> GetHeartRateTimeSeriesAsync(string date, PeriodType period, string userId = Defaults.CurrentUser)
        {
            return GetAsync<HeartRateTimeSeries>($"https://api.fitbit.com/1/user/-/activities/heart/date/{date}/{period.ToUrlParameter()}.json");
        }

        public Task<object> GetHeartRateTimeSeriesAsync(DateTime baseDate, DateTime endDate, string userId = Defaults.CurrentUser)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetHeartRateTimeSeriesAsync(string baseDate, string endDate, string userId = Defaults.CurrentUser)
        {
            throw new NotImplementedException();
        }
    }
}
