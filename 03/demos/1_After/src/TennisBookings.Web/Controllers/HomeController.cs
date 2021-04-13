using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.External.Models;
using TennisBookings.Web.Services;
using TennisBookings.Web.ViewModels;

namespace TennisBookings.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherForecaster _weatherForecaster;
        private readonly FeaturesConfiguration _featuresConfiguration;

        public HomeController(IWeatherForecaster weatherForecaster,
            IOptions<FeaturesConfiguration> options)
        {
            _weatherForecaster = weatherForecaster;
            _featuresConfiguration = options.Value;
        }

        [Route("")]
        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();

            if (_featuresConfiguration.EnabledWeatherForecast)
            {
                var currentWeather = _weatherForecaster.GetCurrentWeather();

                switch (currentWeather.WeatherCondition)
                {
                    case WeatherCondition.Sun:
                        viewModel.WeatherDescription = "It's sunny right now. A great day for tennis.";
                        break;

                    case WeatherCondition.Rain:
                        viewModel.WeatherDescription = "We're sorry but it's raining here. No outdoor courts in use.";
                        break;

                    default:
                        viewModel.WeatherDescription = "We don't have the latest weather information right now, please check again later.";
                        break;
                }
            }

            return View(viewModel);
        }
    }
}
