using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonPedia.Api.Contracts;
using PokemonPedia.Application.Exceptions;
using PokemonPedia.Application.Interfaces;
using PokemonPedia.Application.Model;
using System.Threading.Tasks;

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
        /// Get pokemon base information with a standard translation
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("/[controller]/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            try
            {
                var result = await _pokemonService.GetInfo(name);
                return Ok(_mapper.Map<PokemonResponse>(result));
            }
            catch (PokemonNotFoundException)
            {
                return ErrorResponseFor(name);
            }
        }

        private IActionResult ErrorResponseFor(string name)
        {
            return NotFound(new ErrorResponse()
            {
                ErrorCode = Constants.POKEMON_NOT_FOUND,
                ErrorMessage = $"Pokemon with name {name} has not been found"
            });
        }

        /// <summary>
        /// Get pokemon base information with a fun translation
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> Translate(string name)
        {
            try
            {
                var result = await _pokemonService.GetFunInfo(name);
                return Ok(_mapper.Map<PokemonResponse>(result));
            }
            catch (PokemonNotFoundException)
            {
                return ErrorResponseFor(name);
            }
        }

    }
}
