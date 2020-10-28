//using Brenda.Controllers;
//using Brenda.Jokes;
//using Brenda.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TechTalk.SpecFlow;
//using TechTalk.SpecFlow.Assist;

//namespace Brenda.IntegrationTests
//{
//    [Binding]
//    public class ForecastSteps
//    {
//        private readonly BrendaContext _context;
//        private readonly Mock<IJokeProvider> _jokeProvider;
//        private readonly WeatherForecastController _forecastController;
//        private readonly ScenarioContext _scenarioContext;

//        public ForecastSteps(ScenarioContext scenarioContext)
//        {
//            var options = new DbContextOptionsBuilder<BrendaContext>()
//                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                  .Options;
//            _context = new BrendaContext(options);
//            _jokeProvider = new Mock<IJokeProvider>();
//            _forecastController = new WeatherForecastController(Mock.Of<ILogger<WeatherForecastController>>(), _context, _jokeProvider.Object);
//            _scenarioContext = scenarioContext;
//        }

//        [Given(@"I know about the following forecast")]
//        public async Task GivenIKnowAboutTheFollowingForecastAsync(Table table)
//        {
//            var forecasts = table.CreateSet<Forecast>();
//            await _context.Forecasts.AddRangeAsync(forecasts);
//            await _context.SaveChangesAsync();
//        }

//        [Given(@"And my joke provider has given me these jokes")]
//        public void GivenAndMyJokeProviderHasGivenMeTheseJokes(Table table)
//        {
//            var jokes = table.Rows.Select(r => r[0]).ToArray();
//            _jokeProvider.Setup(p => p.GetJokes(It.IsAny<int>()))
//                .ReturnsAsync(jokes.ToArray());
//        }

//        [When(@"I ask Brenda for a forecast")]
//        public async Task WhenIAskBrendaForAForecastAsync()
//        {
//            var result = await _forecastController.GetAsync().ConfigureAwait(false);
//            _scenarioContext.Add("result", result);
//        }

//        [Then(@"the results are")]
//        public void ThenTheResultsAre(Table table)
//        {
//            var results = _scenarioContext.Get<IEnumerable<WeatherForecast>>("result");
//            table.CompareToSet(results, true);
//        }


//    }
//}
