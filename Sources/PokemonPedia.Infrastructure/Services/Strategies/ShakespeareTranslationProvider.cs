using Microsoft.Extensions.Configuration;
using PokemonPedia.Core.Interfaces;
using System.Threading.Tasks;

namespace PokemonPedia.Infrastructure.Services.Strategies
{
    public class ShakespeareTranslationProvider : ITranslationProvider
    {
        private readonly TranslationClient _translationClient;
        private readonly IConfiguration _configuration;

        public ShakespeareTranslationProvider(TranslationClient translationClient, IConfiguration configuration)
        {
            _translationClient = translationClient;
            _configuration = configuration;
        }

        public async Task<string> ProvideTranslation(string description)
        {
            return await _translationClient
                 .TranslationFor(description, _configuration["TranslationProviderApi:ShakespeareApiUrl"]);
        }
    }
}
