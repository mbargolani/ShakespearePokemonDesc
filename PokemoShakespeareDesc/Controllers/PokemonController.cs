using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokemonShakespeareDesc.BL;
using PokemoShakespeareDesc.Models;

namespace PokemoShakespeareDesc.Controllers
{
    public class PokemonController : ApiController
    {
        private PokeApiProvider _pokeProvider;

        private ShakespeareApiProvider _shakespeareProvider;

        public PokemonController(PokeApiProvider pokemonProvider, ShakespeareApiProvider shakespeareProvider)
        {
            _pokeProvider = pokemonProvider;
            _shakespeareProvider = shakespeareProvider;
        }


        

        [Route("api/pokemon/{name}")]
        public HttpResponseMessage Get(string name)
        {
            try
            {
                var pokemonFlavour = _pokeProvider.GetPokemonDescription(name);

                var desc = _shakespeareProvider.GetShakespeareTranslation(pokemonFlavour, name);

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(desc), Encoding.UTF8, "application/json");
                return response;
            }
            catch (NotFoundException e)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                response.Content = new StringContent(JsonConvert.SerializeObject(e.Message), Encoding.UTF8, "application/json");
                return response;
            }
            catch (TooManyRequestsException e)
            {
                var response = Request.CreateResponse(HttpStatusCode.ServiceUnavailable);
                response.Content = new StringContent(JsonConvert.SerializeObject(e.Message), Encoding.UTF8, "application/json");
                return response;
            }
        }

    }

}
