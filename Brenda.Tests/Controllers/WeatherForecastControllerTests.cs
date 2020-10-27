using AutoFixture;
using Brenda.Controllers;
using Brenda.Jokes;
using Brenda.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Brenda.Tests
{
    public class WeatherForecastControllerTests
    {
        private readonly WeatherForecastController _controller;
        private readonly BrendaContext _context;
        private readonly Mock<IJokeProvider> _jokeProvider;

        public WeatherForecastControllerTests()
        {
            var options = new DbContextOptionsBuilder<BrendaContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            _context = new BrendaContext(options);

            _jokeProvider = new Mock<IJokeProvider>();
            _controller = new WeatherForecastController(
                Mock.Of<ILogger<WeatherForecastController>>(), _context, _jokeProvider.Object);
        }

        [Fact]
        public async Task ShouldReturnForecast()
        {
            Fixture fixture = new Fixture();
            List<Forecast> q = new List<Forecast>(fixture.CreateMany<Forecast>(25));
            var jokes = fixture.CreateMany<string>(25).ToArray();
            _context.Forecasts.AddRange(q);
            _context.SaveChanges();

            //Act
            var result = await _controller.GetAsync();

            //Assert
            var i = 0;
            var forecasts = q.ToArray();
            foreach (var item in result)
            {
                Assert.Equal(jokes[i], item.Summary);
                Assert.Equal(forecasts[i].Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), item.Date);
                Assert.Equal(forecasts[i].TemperatureC, item.TemperatureC);
                i++;
            }
        }
    }
}
