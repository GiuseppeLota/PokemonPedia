using PokemonPedia.Application.Exceptions;
using PokemonPedia.Application.Interfaces;
using PokemonPedia.Application.Model;
using PokemonPedia.Core.Interfaces;

namespace PokemonPedia.Application.Services
{
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

        public PokemonResult FetchPokemon(string name)
        {
            var response = _pokemonProvider.GetPokemon(name);

            if (response.Result == null)
                throw new PokemonNotFoundException();

            var pokemon = response.Result;

            var translationProvider = _translationResolver.GetTranslationProvider(pokemon.Habitat);

            var translatedDescription = translationProvider
                .ProvideTranslation(pokemon.RawDescription)
                .Result;

            return new PokemonResult()
            {
                Description = translatedDescription,
                Habitat = pokemon.Habitat,
                IsLegendary = pokemon.IsLegendary,
                Name = pokemon.Name
            };
        }
    }
}
