using Moq;
using PokemonPedia.Application.Services;
using PokemonPedia.Core.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace PokemonPedia.Application.Tests.Services
{
    public class TranslationResolverTests
    {
        private readonly ITranslationProvider _shakespeareTranslationProvider;
        private readonly ITranslationProvider _yodaTranslationProvider;
        private readonly List<ITranslationProvider> _providers;

        public TranslationResolverTests()
        {
            _shakespeareTranslationProvider = Mock.Of<ITranslationProvider>(_=> _.TranslationName == TranslationName.Shakespeare);
            _yodaTranslationProvider = Mock.Of<ITranslationProvider>(_ => _.TranslationName == TranslationName.Yoda);

            _providers = new List<ITranslationProvider>() { _shakespeareTranslationProvider, _yodaTranslationProvider };
        }

        /// <summary>
        /// If the pokemon habitat is cave, the translation should be provided by the Yoda translation provider
        /// </summary>
        [Fact]
        public void Pokemon_habitat_cave_yoda_tranlation()
        {
            var translationResolver = new TranslationResolver(_providers);

            var translationProvider = translationResolver.GetTranslationProvider("cave");

            Assert.Equal(TranslationName.Yoda, translationProvider.TranslationName);
        }

        /// <summary>
        /// If the pokemon habitat is not cave, the translation should be provided by the Shakespeare translation provider
        /// </summary>
        [Fact]
        public void Pokemon_habitat_not_cave_Shakespeare_tranlation()
        {
            var translationResolver = new TranslationResolver(_providers);

            var translationProvider = translationResolver.GetTranslationProvider("air");

            Assert.Equal(TranslationName.Shakespeare, translationProvider.TranslationName);
        }


    }
}
