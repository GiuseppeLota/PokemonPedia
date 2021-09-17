using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonPedia.Core.Interfaces
{
    /// <summary>
    /// Encapsulate the Translation resolver logic
    /// </summary>
    public interface ITranslationResolver
    {
        /// <summary>
        /// Resolve the correct translation provider according to the incoming habitat parameter
        /// </summary>
        /// <param name="habitat"></param>
        /// <returns></returns>
        ITranslationProvider GetTranslationProvider(string habitat);
    }
}
