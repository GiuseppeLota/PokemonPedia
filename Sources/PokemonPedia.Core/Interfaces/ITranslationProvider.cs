using System.Threading.Tasks;

namespace PokemonPedia.Core.Interfaces
{
    /// <summary>
    /// Provider for pokemon translations
    /// </summary>
    public interface ITranslationProvider
    {
        /// <summary>
        /// Provide a description translation
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public Task<string> ProvideTranslation(string description);

        /// <summary>
        /// Translation provider name
        /// </summary>
        TranslationName TranslationName { get; set; }
    }

    public enum TranslationName
    {
        Shakespeare,
        Yoda,
    }
}
