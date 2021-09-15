using PokemonPedia.Core.Entities;
using System.Threading.Tasks;

namespace PokemonPedia.Core.Interfaces
{
    public interface IPokemonProvider
    {
        /// <summary>
        /// Get Pokemon basic informations
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns></returns>
        public Task<PokemonData> GetPokemon(string pokemonName);
    }
}
