namespace PokemonPedia.Application.Model
{
    /// <summary>
    /// Pokemon result object from the api
    /// </summary>
    public class PokemonResult
    {
        /// <summary>
        /// Name of the pokemon
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Translated pokemon description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Habitat of the pokemon
        /// </summary>
        public string Habitat { get; set; }

        /// <summary>
        /// Flag to indicate wheter the pokemon is legendary
        /// </summary>
        public bool IsLegendary { get; set; }
    }
}
