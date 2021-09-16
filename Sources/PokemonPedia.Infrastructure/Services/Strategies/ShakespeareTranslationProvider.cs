using PokemonPedia.Core.Interfaces;
using System.Threading.Tasks;

namespace PokemonPedia.Infrastructure.Services.Strategies
{
    public class ShakespeareTranslationProvider : ITranslationProvider
    {
        private readonly TranslationClient _translationClient;

        public ShakespeareTranslationProvider(TranslationClient translationClient)
        {
            _translationClient = translationClient;
        }

        public async Task<string> ProvideTranslation(string description)
        {
            return await _translationClient
                 .TranslationFor(description, "https://api.funtranslations.com/translate/shakespeare.json");
        }
    }
}
