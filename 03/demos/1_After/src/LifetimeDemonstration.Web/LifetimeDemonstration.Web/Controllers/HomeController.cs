using Microsoft.AspNetCore.Mvc;
using LifetimeDemonstration.Web.Services;
using Microsoft.Extensions.Logging;

namespace LifetimeDemonstration.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly GuidService _guidService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(GuidService guidService, ILogger<HomeController> logger)
        {
            _guidService = guidService;
            _logger = logger;
        }

        [Route("")]
        public IActionResult Index()
        {
            var guid = _guidService.GetGuid();

            var logMessage = $"Controller: The GUID from the GuidService is {guid}";
            
            _logger.LogInformation(logMessage);

            return Ok();
        }
    }
}
