using PokemonPedia.Core.Interfaces;
using System.Threading.Tasks;

namespace PokemonPedia.Infrastructure.Services.Strategies
{
    public class YodaTranslationProvider : ITranslationProvider
    {
        private readonly TranslationClient _translationClient;

        public YodaTranslationProvider(TranslationClient translationClient)
        {
            _translationClient = translationClient;
        }

        public async Task<string> ProvideTranslation(string description)
        {
            return await _translationClient
                .TranslationFor(description, "https://api.funtranslations.com/translate/joda.json");
        }
    }
}
