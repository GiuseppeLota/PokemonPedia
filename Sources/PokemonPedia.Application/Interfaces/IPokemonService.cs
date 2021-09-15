using PokemonPedia.Application.Model;

namespace PokemonPedia.Application.Interfaces
{
    public interface IPokemonService
    {
        public PokemonResult FetchPokemon(string name);
    }
}
