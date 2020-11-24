using Microsoft.CSharp.RuntimeBinder;
using PokemoShakespeareDesc.Controllers;
using PokemoShakespeareDesc.Interfaces;

namespace PokemonShakespeareDesc.BL
{
    public class PokeApiProvider : IPokeApiProvider
    {
        private readonly IHttpHelper _httpHelper;

        private const string BaseUri = "https://pokeapi.co/api/v2/";
        
        public const string PokemonSpeciesRequest ="pokemon-species";

        public PokeApiProvider(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public string GetPokemonDescription(string pokeName)
        {
            try
            {
                var pokemonId = GetPokemonId(pokeName);
                if (pokemonId == null) throw new NotFoundException($"Cannot find the specified Pokemon: {pokeName}");
                var species = _httpHelper.Get($"{BaseUri}{PokemonSpeciesRequest}/{pokemonId}").Result;

                var entries = species.flavor_text_entries;

                if (entries != null)
                {
                    string ret = entries[0].flavor_text.ToString(); // assumption - I am taking the first available flavour, as it wasn't specified in the requirements
                                                                    // whether it is need to use a specific flavor of pokemon.

                    return ret.Replace("\n", " ").Replace("\f", " "); // I am removing the \n and \f for the text to be readable by a human

                }

                
            }
            catch (RuntimeBinderException e) // this is the exception thrown when a dynamic object can't find the property I was trying to bind
            {
                // I am returning null instead of allowing a general exception to be thrown which won't tell user what is wrong
            }

            return null;

        }

        

        private string GetPokemonId(string pokeName)
        {
            try
            {
                var pokemon = _httpHelper.Get($"{BaseUri}pokemon/{pokeName}").Result;
                return pokemon.id.ToString();
            }
            catch (RuntimeBinderException e) // this is the exception thrown when a dynamic object can't find the property I was trying to bind
            {

                return null;
            }
            
        }
    }

    
}