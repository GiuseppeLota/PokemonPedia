using PokemonPedia.Core.Interfaces;
using System.Threading.Tasks;

namespace PokemonPedia.Infrastructure.Services.Strategies
{
    public class ShakespeareTranslationProvider : ITranslationProvider
    {
        public async Task<string> ProvideTranslation(string description)
        {
            return await new TranslationClient()
                 .TranslationFor(description, "https://api.funtranslations.com/translate/shakespeare.json");
        }
    }
}
