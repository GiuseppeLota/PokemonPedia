using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonPedia.Api.Contracts;
using PokemonPedia.Application.Exceptions;
using PokemonPedia.Application.Interfaces;
using PokemonPedia.Application.Model;

namespace PokemonPedia.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonService pokemonService, IMapper mapper)
        {
            _pokemonService = pokemonService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get detailed information for a provided pokemon name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public IActionResult Translate(string name)
        {
            try
            {
                var result = _pokemonService.FetchPokemon(name);
                return Ok(_mapper.Map<PokemonResponse>(result));
            }
            catch (PokemonNotFoundException)
            {
                return NotFound(new ErrorResponse()
                {
                    ErrorCode = Constants.POKEMON_NOT_FOUND,
                    ErrorMessage = $"Pokemon with name {name} has not been found"
                });
            }
        }

    }
}
