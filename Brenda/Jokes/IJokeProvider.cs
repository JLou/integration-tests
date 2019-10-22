using System.Threading.Tasks;

namespace Brenda.Jokes
{
    public interface IJokeProvider
    {
        Task<string[]> GetJokes(int number);
    }
}