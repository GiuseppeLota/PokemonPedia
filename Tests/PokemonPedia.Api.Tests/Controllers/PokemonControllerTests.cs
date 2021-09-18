using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PokemonPedia.Api.Contracts;
using PokemonPedia.Api.Controllers;
using PokemonPedia.Api.Mapper;
using PokemonPedia.Application.Exceptions;
using PokemonPedia.Application.Interfaces;
using PokemonPedia.Application.Model;
using Xunit;

namespace PokemonPedia.Api.Tests.Controllers
{
    public class PokemonControllerTests
    {
        private const string POKEMON_NAME = "POKEMON_NAME_TEST";
        private const string POKEMON_DESCRIPTION = "POKEMON_DESCRIPTION";
        private const string POKEMON_HABITAT = "POKEMON_HABITAT";
        private readonly ServiceProvider _serviceProvider;
        private readonly Mock<IPokemonService> _pokemonService;

        public PokemonControllerTests()
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(cfg => cfg.AddProfile(new PokemonPediaProfile()), typeof(PokemonControllerTests));

            _serviceProvider = services.BuildServiceProvider();
            _pokemonService = new Mock<IPokemonService>();
        }

        [Fact]
        public void Pokemon_fetched_response200()
        {
            var pokemon = new PokemonResult()
            {
                Description = POKEMON_DESCRIPTION,
                Habitat = POKEMON_HABITAT,
                IsLegendary = true,
                Name = POKEMON_NAME
            };

            _pokemonService.Setup(x => x.FetchPokemon(POKEMON_NAME)).Returns(pokemon);

            var controller = new PokemonController(_pokemonService.Object, _serviceProvider.GetRequiredService<IMapper>());

            var response = controller.Translate(POKEMON_NAME);

            Assert.IsType<OkObjectResult>(response);

            var result = ((OkObjectResult)response).Value as PokemonResponse;

            Assert.Equal(POKEMON_DESCRIPTION, result.Description);
            Assert.Equal(POKEMON_HABITAT, result.Habitat);
            Assert.Equal(POKEMON_NAME, result.Name);
            Assert.True(result.IsLegendary);
        }

        [Fact]
        public void Pokemon_not_fetched_response404() 
        {
            _pokemonService.Setup(x => x.FetchPokemon(It.IsAny<string>())).Throws(new PokemonNotFoundException());

            var controller = new PokemonController(_pokemonService.Object, _serviceProvider.GetRequiredService<IMapper>());

            var response = controller.Translate(POKEMON_NAME);

            Assert.IsType<NotFoundObjectResult>(response);

            var result = ((NotFoundObjectResult)response).Value as ErrorResponse;

            Assert.Equal(Constants.POKEMON_NOT_FOUND, result.ErrorCode);
        }
    }
}
