using PokemonPedia.Application.Model;
using System.Threading.Tasks;

namespace PokemonPedia.Application.Interfaces
{
    /// <summary>
    /// Pokemon Application Service
    /// </summary>
    public interface IPokemonService
    {

        /// <summary>
        /// Get Standard pokemon information
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<PokemonResult> GetInfo(string name);


        /// <summary>
        /// Get basic pokemon information with a fun description
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<PokemonResult> GetFunInfo(string name);

    }
}
