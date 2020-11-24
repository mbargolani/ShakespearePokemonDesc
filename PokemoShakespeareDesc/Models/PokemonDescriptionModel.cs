using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemoShakespeareDesc.Models
{
    /// <summary>
    /// The model of the Pokemon description in Shakespeare's English
    /// </summary>
    public class PokemonDescriptionModel
    {
       
        /// <summary>
        /// Pokemon name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Pokemon Description translated to Shakespeare's English
        /// </summary>
        public string Description { get; set; }
        
    }
}