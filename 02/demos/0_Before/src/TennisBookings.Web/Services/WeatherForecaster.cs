
using TennisBookings.Web.External.Models;

namespace TennisBookings.Web.Services
{
    public class WeatherForecaster
    {
        public WeatherResult GetCurrentWeather()
        {
            // Pretend we call out to a remote 3rd party API here to get the real forecast
            // For demo purposes. the result is hardcoded.

            return new WeatherResult
            {
                WeatherCondition = WeatherCondition.Sun
            };
        }
    }
}
