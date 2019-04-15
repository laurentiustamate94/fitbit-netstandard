using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fitbit.Api.Abstractions.Models.HeartRate;

namespace Fitbit.Api.Abstractions.Endpoints
{
    public interface IHeartRate
    {
        Task<object> GetHeartRateTimeSeriesAsync(DateTime date, PeriodType period, string userId = Defaults.CurrentUser);

        Task<object> GetHeartRateTimeSeriesAsync(string date, PeriodType period, string userId = Defaults.CurrentUser);

        Task<object> GetHeartRateTimeSeriesAsync(DateTime baseDate, DateTime endDate, string userId = Defaults.CurrentUser);

        Task<HeartRateTimeSeries> GetHeartRateTimeSeriesAsync(string baseDate, string endDate, string userId = Defaults.CurrentUser);

        Task<object> GetHeartRateIntradayTimeSeriesAsync();
    }
}
