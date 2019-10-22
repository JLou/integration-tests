using Brenda.Jokes;
using Brenda.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brenda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly BrendaContext _context;
        private readonly IJokeProvider _jokeProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, BrendaContext context,
            IJokeProvider jokeProvider)
        {
            _logger = logger;
            _context = context;
            _jokeProvider = jokeProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            var rng = new Random();

            var jokesTasks = _jokeProvider.GetJokes(25);
            var forecastTasks = _context.Forecasts.Select(f => new WeatherForecast
            {
                Date = f.Date,
                TemperatureC = f.TemperatureC,
            })
            .OrderBy(f => f.Date)
            .Take(25)
            .ToArrayAsync();

            var jokes = await jokesTasks;
            var forecasts = await forecastTasks;

            return forecasts.Zip(jokes, (WeatherForecast f, string j) => { f.Summary = j; return f; });
        }
    }
}
