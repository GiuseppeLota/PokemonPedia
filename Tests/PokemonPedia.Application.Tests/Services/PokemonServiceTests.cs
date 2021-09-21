using Moq;
using PokemonPedia.Application.Exceptions;
using PokemonPedia.Application.Services;
using PokemonPedia.Core.Interfaces;
using Xunit;

namespace PokemonPedia.Application.Tests.Services
{
    public class PokemonServiceTests
    {
        private const string POKEMON_NAME = "TEST_NAME";
        private const string POKEMON_HABITAT = "TEST_HABITAT";
        private const string POKEMON_RAW_DESCRIPTION = "TEST_DESCRIPTION";
        private const string POKEMON_TRANSLATED_DESCRIPTION = "TRANSLATED_DESCRIPTION";

        private readonly Mock<IPokemonProvider> _mockPokemonProvider;
        private readonly Mock<ITranslationResolver> _ITranslationResolver;
        private readonly Mock<ITranslationProvider> _ITranlsationProvider;

        public PokemonServiceTests()
        {
            _mockPokemonProvider = new Mock<IPokemonProvider>();
            _ITranslationResolver = new Mock<ITranslationResolver>();
            _ITranlsationProvider = new Mock<ITranslationProvider>();
        }

        /// <summary>
        /// Get info operation: case pokemon is found
        /// </summary>
        [Fact]
        public async void GetInfo_pokemon_found()
        {
            _mockPokemonProvider.Setup(x => x.GetPokemon(POKEMON_NAME)).ReturnsAsync(new Core.Entities.PokemonData() { Habitat = POKEMON_HABITAT, IsLegendary = true, Name = POKEMON_NAME, RawDescription = POKEMON_RAW_DESCRIPTION });

            var pokemonService = new PokemonService(_ITranslationResolver.Object, _mockPokemonProvider.Object);

            var result = await pokemonService.GetInfo(POKEMON_NAME);

            Assert.Equal(result.Habitat, POKEMON_HABITAT);
            Assert.Equal(result.Description, POKEMON_RAW_DESCRIPTION);
            Assert.Equal(result.Name, POKEMON_NAME);
            Assert.True(result.IsLegendary);
        }

        /// <summary>
        /// Get info operation: case pokemon is not found
        /// </summary>
        [Fact]
        public void GetInfo_pokemon_not_found()
        {
            _mockPokemonProvider.Setup(x => x.GetPokemon(POKEMON_NAME));

            var pokemonService = new PokemonService(_ITranslationResolver.Object, _mockPokemonProvider.Object);

            Assert.ThrowsAsync<PokemonNotFoundException>(() => pokemonService.GetInfo(POKEMON_NAME));
        }

        /// <summary>
        /// A pokemon is found with habitat information
        /// </summary>
        [Fact]
        public async void GetFunInfo_pokemon_found()
        {
            _mockPokemonProvider.Setup(x => x.GetPokemon(POKEMON_NAME)).ReturnsAsync(new Core.Entities.PokemonData() { Habitat= POKEMON_HABITAT, IsLegendary=true, Name= POKEMON_NAME, RawDescription= POKEMON_RAW_DESCRIPTION });
            _ITranlsationProvider.Setup(x => x.ProvideTranslation(POKEMON_RAW_DESCRIPTION)).ReturnsAsync(POKEMON_TRANSLATED_DESCRIPTION);
            _ITranslationResolver.Setup(x => x.GetTranslationProvider(POKEMON_HABITAT)).Returns(_ITranlsationProvider.Object);

            var pokemonService = new PokemonService(_ITranslationResolver.Object, _mockPokemonProvider.Object);

            var result = await pokemonService.GetFunInfo(POKEMON_NAME);

            Assert.Equal(result.Habitat, POKEMON_HABITAT);
            Assert.Equal(result.Description, POKEMON_TRANSLATED_DESCRIPTION);
            Assert.Equal(result.Name, POKEMON_NAME);
            Assert.True(result.IsLegendary);
        }

        /// <summary>
        /// Pokemon not found: it should raise a specific expection
        /// </summary>
        [Fact]
        public void GetFunInfo_pokemon_not_found()
        {
            _mockPokemonProvider.Setup(x => x.GetPokemon(POKEMON_NAME));

            var pokemonService = new PokemonService(_ITranslationResolver.Object, _mockPokemonProvider.Object);

            Assert.ThrowsAsync<PokemonNotFoundException>(() => pokemonService.GetFunInfo(POKEMON_NAME));
        }
    }
}
