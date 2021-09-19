using PokemonPedia.Application.Model;

namespace PokemonPedia.Application.Interfaces
{
    /// <summary>
    /// Pokemon Application Service
    /// </summary>
    public interface IPokemonService
    {
        /// <summary>
        /// Get an aggregated information for the provided pokemon name and the resolved translation
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PokemonResult FetchPokemon(string name);
    }
}
