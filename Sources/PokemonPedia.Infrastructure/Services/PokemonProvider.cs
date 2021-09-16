using PokemonPedia.Core.Entities;
using PokemonPedia.Core.Interfaces;
using PokemonPedia.Infrastructure.Data;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;

namespace PokemonPedia.Infrastructure.Services
{
    public class PokemonProvider : IPokemonProvider
    {
        private readonly IConfiguration _configuration;

        public PokemonProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PokemonData> GetPokemon(string pokemonName)
        {
            using var client = new HttpClient();

            var pokeUri = string.Concat(_configuration["PokemonApiUrl"], $"/{pokemonName}");

            var result = await client.GetAsync(pokeUri);

            if (!result.IsSuccessStatusCode)
                return default;

            var pokemonData = JsonSerializer.Deserialize<ExternalPokemonModel>(result.Content.ReadAsStringAsync().Result);

            return new PokemonData()
            {
                Habitat = pokemonData.Habitat.Name,
                IsLegendary = pokemonData.IsLegendary,
                Name = pokemonData.Name,
                RawDescription = PokemonDescriptionFor(pokemonData)
            };
        }

        private string PokemonDescriptionFor(ExternalPokemonModel pokemonData)
        {
            var preferredLanguage = _configuration["PreferredLanguage"];

            return pokemonData
                            .FlavorTextEntries
                            .Where(it => it.Language.Name.Equals(preferredLanguage, StringComparison.InvariantCultureIgnoreCase))
                            .Select(el => el.FlavorText)
                            .FirstOrDefault();
        }

    }
}
