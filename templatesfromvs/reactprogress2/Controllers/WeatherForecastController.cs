using Microsoft.AspNetCore.Mvc;

namespace reactprogress2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                //data comes through this controller qqqq
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Dataquieries.Dataqueries.GetAnyData(),//Random.Shared.Next(-20, 55),
                Summary = Dataquieries.Dataqueries.GetAnyName()
            })
            .ToArray();
        }
    }
}