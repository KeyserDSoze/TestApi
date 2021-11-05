using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi.Attributes;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Manager Manager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            Manager manager)
        {
            _logger = logger;
            Manager = manager;
        }

        [HttpGet]
        public string Get(string id)
            => @$"Singleton1 {Manager.Service1} - Singleton2 {Manager.Service2} - Scoped1 {Manager.Service3} -
                    Scoped2 {Manager.Service4} - Transient1 {Manager.Service5} - Transient2 {Manager.Service6} -
                    BestService {Manager.Service7.Id} - Behavior {Manager.Behavior}";
        [HttpGet]
        public IEnumerable<WeatherForecast> Get2()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        public IEnumerable<WeatherForecast> Get3([FromBody] WeatherForecast forecast)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
