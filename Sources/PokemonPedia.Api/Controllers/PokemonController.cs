using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonPedia.Api.Contracts;
using PokemonPedia.Application.Exceptions;
using PokemonPedia.Application.Interfaces;

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
                return NotFound(name);
            }
        }
    }
}
