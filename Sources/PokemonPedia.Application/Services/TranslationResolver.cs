using PokemonPedia.Core.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PokemonPedia.Application.Services
{
    /// <inheritdoc/>
    public class TranslationResolver : ITranslationResolver
    {
        private const string CAVE_HABITAT = "cave";

        private readonly IEnumerable<ITranslationProvider> _translationProviders;

        public TranslationResolver(IEnumerable<ITranslationProvider> translationProviders)
        {
            _translationProviders = translationProviders;
        }

        /// <inheritdoc/>
        public ITranslationProvider GetTranslationProvider(string habitat)
        {
            return habitat switch
            {
                CAVE_HABITAT => _translationProviders
                            .Where(prov => prov.TranslationName.Equals(TranslationName.Yoda))
                            .FirstOrDefault(),
                _ => _translationProviders
                            .Where(prov => prov.TranslationName.Equals(TranslationName.Shakespeare))
                            .FirstOrDefault(),
            };
        }

    }
}
