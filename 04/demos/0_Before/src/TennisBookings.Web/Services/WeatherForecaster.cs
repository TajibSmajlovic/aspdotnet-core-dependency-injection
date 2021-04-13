using System.Threading.Tasks;
using TennisBookings.Web.Domain;
using TennisBookings.Web.External;

namespace TennisBookings.Web.Services
{
    public class WeatherForecaster : IWeatherForecaster
    {
        private readonly IWeatherApiClient _weatherApiClient;

        public WeatherForecaster(IWeatherApiClient weatherApiClient)
        {
            _weatherApiClient = weatherApiClient;
        }

        public async Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            var currentWeather = await _weatherApiClient.GetWeatherForecastAsync();

            var result = new CurrentWeatherResult
            {
                Description = currentWeather.Weather.Description
            };

            return result;
        }
    }
}
