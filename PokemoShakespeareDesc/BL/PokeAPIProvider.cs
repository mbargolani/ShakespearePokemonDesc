using Microsoft.CSharp.RuntimeBinder;
using PokemoShakespeareDesc.Controllers;
using PokemoShakespeareDesc.Interfaces;

namespace PokemonShakespeareDesc.BL
{
    public class PokeApiProvider : IPokeApiProvider
    {
        private static readonly string _baseUri = "https://pokeapi.co/api/v2/";

        private readonly IHttpHelper _httpHelper;

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
                var species = _httpHelper.Get($"{_baseUri}pokemon-species/{pokemonId}").Result;

                var entries = species.flavor_text_entries;

                if (entries != null)
                {
                    string ret = entries[0].flavor_text.ToString();
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
                var pokemon = _httpHelper.Get($"{_baseUri}pokemon/{pokeName}").Result;
                return pokemon.id.ToString();
            }
            catch (RuntimeBinderException e) // this is the exception thrown when a dynamic object can't find the property I was trying to bind
            {

                return null;
            }
            
        }
    }

    
}