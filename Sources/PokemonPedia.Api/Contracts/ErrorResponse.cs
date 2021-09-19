
namespace PokemonPedia.Api.Contracts
{

    /// <summary>
    /// Api error response
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Error code
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Error description
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
