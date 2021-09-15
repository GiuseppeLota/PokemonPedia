using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
