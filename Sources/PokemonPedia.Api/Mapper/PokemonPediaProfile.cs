using AutoMapper;
using PokemonPedia.Api.Contracts;
using PokemonPedia.Application.Model;

namespace PokemonPedia.Api.Mapper
{
    public class PokemonPediaProfile : Profile
    {
        public PokemonPediaProfile() 
        {
            CreateMap<PokemonResponse, PokemonResult>().ReverseMap();
        }
    }
}
