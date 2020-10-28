using Brenda.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;


namespace Brenda.IntegrationTests
{
    [Binding]
    public class ForecastApiSteps
    {
        private readonly FeatureContext _featureContext;
        private readonly HttpClient _client;
        private HttpResponseMessage _result;

        public ForecastApiSteps(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            TestingApplicationFactory<Startup> factory = featureContext["factory"] as TestingApplicationFactory<Brenda.Startup>;
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "Test", options => { });
                });
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                HandleCookies = false
            });
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            featureContext["factory"] = new TestingApplicationFactory<Brenda.Startup>();
        }

        [Given(@"I know about the following forecast")]
        public void GivenIKnowAboutTheFollowingForecast(Table table)
        {
            TestingApplicationFactory<Startup> factory = _featureContext["factory"] as TestingApplicationFactory<Brenda.Startup>;
            using (var db = factory.Context)
            {
                var records = table.CreateSet<Forecast>();
                db.Forecasts.RemoveRange(db.Forecasts);
                db.Forecasts.AddRange(records);
                db.SaveChanges();
            }
        }

        [Given(@"And my joke provider will give me these jokes in order")]
        public void GivenAndMyJokeProviderWillGiveMeTheseJokesInOrder(Table table)
        {
            TestingApplicationFactory<Startup> factory = _featureContext["factory"] as TestingApplicationFactory<Brenda.Startup>;
            foreach (var joke in table.Rows.Select(r => r[0]))
            {
                factory.HttpMessageHandler.Responses.Enqueue(joke);
            }
        }

        [Given(@"I am authenticated")]
        public void GivenIAmAuthenticated()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("test");
        }

        [Given(@"I am not authenticated")]
        public void GivenIAmNotAuthenticated()
        {
            _client.DefaultRequestHeaders.Authorization = null;
        }

        [When(@"I ask Brenda for a forecast")]
        public async Task WhenIAskBrendaForAForecastAsync()
        {
            _result = await _client.GetAsync("/api/weather-forecast");
        }

        [Then(@"My api returns a (.*) status code")]
        public void ThenMyApiReturnsA4302StatusCode(int statusCode)
        {
            _result.StatusCode.Should().Be((HttpStatusCode)statusCode);
        }

        //[Then(@"My api returns a 200 status code")]
        //public void ThenMyApiReturnsA200StatusCode()
        //{
        //    _result.StatusCode.Should().Be(HttpStatusCode.OK);
        //}

        [Then(@"the results are")]
        public async Task ThenTheResultsAreAsync(Table table)
        {
            var payload = await JsonSerializer.DeserializeAsync<IEnumerable<WeatherForecast>>(await _result.Content.ReadAsStreamAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            table.CompareToSet(payload, true);
        }
    }
}
