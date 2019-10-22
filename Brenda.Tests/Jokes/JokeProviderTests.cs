using AutoFixture;
using Brenda.Jokes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Brenda.Tests.Jokes
{
    public class JokeProviderTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly JokeProvider _provider;
        private readonly Queue<string> _responses;

        public JokeProviderTests()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            StubMessageHandler handler = new StubMessageHandler();
            _httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(handler));
            _responses = handler.Responses;

            _provider = new JokeProvider(_httpClientFactory.Object);
        }

        [Fact]
        public async Task ShouldReturnJokesInOrder()
        {
            Fixture fixture = new Fixture();
            var responses = fixture.CreateMany<string>(10).ToList();
            responses.ForEach(r => _responses.Enqueue(r));

            //Act
            var actual = await _provider.GetJokes(10);

            //Assert
            Assert.Equal(responses.Count, actual.Length);
            Assert.Equal(responses, actual);
        }

        private class StubMessageHandler : HttpMessageHandler
        {
            public Queue<string> Responses { get; private set; } = new Queue<string>();

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (Responses.TryDequeue(out string response))
                {
                    HttpResponseMessage httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(response)
                    };
                    return await Task.FromResult(httpResponse);
                }

                throw new IndexOutOfRangeException();
            }
        }

    }

}
