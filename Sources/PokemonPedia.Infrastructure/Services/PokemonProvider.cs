using PokemonPedia.Core.Entities;
using PokemonPedia.Core.Interfaces;
using PokemonPedia.Infrastructure.Data;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using PokemonPedia.Core.Components;

namespace PokemonPedia.Infrastructure.Services
{
    /// <inheritdoc/>
    public class PokemonProvider : IPokemonProvider
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public PokemonProvider(IConfiguration configuration, PokepediaHttpClientFactory pokepediaHttpClient)
        {
            _configuration = configuration;
            _httpClient = pokepediaHttpClient.GetHttpClient();
        }

        /// <inheritdoc/>
        public async Task<PokemonData> GetPokemon(string pokemonName)
        {
            var pokeUri = string.Concat(_configuration["PokemonApiUrl"], $"/{pokemonName}");

            var result = await _httpClient.GetAsync(pokeUri);

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
