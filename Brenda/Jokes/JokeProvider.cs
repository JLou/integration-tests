using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Brenda.Jokes
{
    public class JokeProvider : IJokeProvider
    {
        private readonly IHttpClientFactory _clientFactory;

        public JokeProvider(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<string[]> GetJokes(int number)
        {
            var client = _clientFactory.CreateClient("with-proxy");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var tasks = Enumerable.Range(0, number).Select(i => this.GetSingleJoke(client));
            return await Task.WhenAll(tasks);
        }

        private async Task<string> GetSingleJoke(HttpClient client)
        {
            var response = await client.GetAsync("https://icanhazdadjoke.com/");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
