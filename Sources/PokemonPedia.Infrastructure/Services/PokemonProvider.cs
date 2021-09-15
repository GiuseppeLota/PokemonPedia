using PokemonPedia.Core.Entities;
using PokemonPedia.Core.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonPedia.Infrastructure.Services
{
    public class PokemonProvider : IPokemonProvider
    {
        public async Task<PokemonData> GetPokemon(string pokemonName)
        {
            using var client = new HttpClient();

            var result = await client.GetAsync(string.Format("https://pokeapi.co/api/v2/pokemon/{0}", pokemonName));


            throw new System.NotImplementedException();
        }
    }
}
