using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Brenda.IntegrationTests
{
    public class IntegrationTests : IClassFixture<TestingApplicationFactory<Brenda.Startup>>
    {
        private readonly TestingApplicationFactory<Startup> _factory;

        public IntegrationTests(TestingApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_WeatherForecast()
        {
            //Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        "Test", options => { });
                });
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            foreach (var joke in Enumerable.Range(1, 5).Select(i => $"joke{i}"))
            {
                _factory.HttpMessageHandler.Responses.Enqueue(joke);
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("test");
            var expectedPayload = new[]
            {
                new WeatherForecast{ Date = "2020-01-01", Summary = "joke1", TemperatureC = 15},
                new WeatherForecast{ Date = "2020-02-01", Summary = "joke2", TemperatureC = 16},
                new WeatherForecast{ Date = "2020-03-01", Summary = "joke3", TemperatureC = 17},
                new WeatherForecast{ Date = "2020-05-01", Summary = "joke4", TemperatureC = 20},
                new WeatherForecast{ Date = "2020-06-01", Summary = "joke5", TemperatureC = 21},
            };

            //Act
            var response = await client.GetAsync("/api/weather-forecast");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.Content.ReadAsStringAsync();
            var payload = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            payload.Should().BeEquivalentTo(expectedPayload);

        }
    }
}
