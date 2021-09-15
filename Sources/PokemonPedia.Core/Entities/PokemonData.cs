using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonPedia.Core.Entities
{
    public class PokemonData
    {
        /// <summary>
        /// Name of the pokemon
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Translated Description of the pokemon
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
