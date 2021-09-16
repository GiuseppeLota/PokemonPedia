using PokemonPedia.Application.Exceptions;
using PokemonPedia.Application.Interfaces;
using PokemonPedia.Application.Model;
using PokemonPedia.Core.Interfaces;
using System;

namespace PokemonPedia.Application.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly Func<string, ITranslationProvider> _traslationProviderAccessor;
        private readonly IPokemonProvider _pokemonProvider;

        public PokemonService(Func<string, ITranslationProvider> traslationProviderAccessor,
            IPokemonProvider pokemonProvider)
        {
            _traslationProviderAccessor = traslationProviderAccessor;
            _pokemonProvider = pokemonProvider;
        }

        public PokemonResult FetchPokemon(string name)
        {
            var response = _pokemonProvider.GetPokemon(name);

            if (response.Result == null)
                throw new PokemonNotFoundException();

            var pokemon = response.Result;

            var translatedDescription = _traslationProviderAccessor(pokemon.Habitat)
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
