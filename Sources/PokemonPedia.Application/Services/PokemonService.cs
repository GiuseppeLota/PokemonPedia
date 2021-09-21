using PokemonPedia.Application.Exceptions;
using PokemonPedia.Application.Interfaces;
using PokemonPedia.Application.Model;
using PokemonPedia.Core.Interfaces;
using System.Threading.Tasks;

namespace PokemonPedia.Application.Services
{
    /// <inheritdoc/>
    public class PokemonService : IPokemonService
    {
        private readonly ITranslationResolver _translationResolver;
        private readonly IPokemonProvider _pokemonProvider;

        public PokemonService(ITranslationResolver translationResolver,
            IPokemonProvider pokemonProvider)
        {
            _translationResolver = translationResolver;
            _pokemonProvider = pokemonProvider;
        }

        /// <inheritdoc/>
        public async Task<PokemonResult> GetFunInfo(string name)
        {
            var pokemon = await _pokemonProvider.GetPokemon(name);

            if (pokemon == null)
                throw new PokemonNotFoundException();

            var translationProvider = _translationResolver.GetTranslationProvider(pokemon.Habitat);

            var translatedDescription = await translationProvider.ProvideTranslation(pokemon.RawDescription);

            return new PokemonResult()
            {
                Description = translatedDescription,
                Habitat = pokemon.Habitat,
                IsLegendary = pokemon.IsLegendary,
                Name = pokemon.Name
            };
        }

        /// <inheritdoc/>
        public async Task<PokemonResult> GetInfo(string name)
        {
            var pokemon = await _pokemonProvider.GetPokemon(name);

            if (pokemon == null)
                throw new PokemonNotFoundException();

            return new PokemonResult()
            {
                Description = pokemon.RawDescription,
                Habitat = pokemon.Habitat,
                IsLegendary = pokemon.IsLegendary,
                Name = pokemon.Name
            };
        }
    }
}
