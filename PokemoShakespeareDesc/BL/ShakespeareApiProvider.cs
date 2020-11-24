using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PokemoShakespeareDesc.Controllers;
using PokemoShakespeareDesc.Interfaces;
using PokemoShakespeareDesc.Models;

namespace PokemonShakespeareDesc.BL
{
    /// <summary>
    /// 
    /// </summary>
    public class ShakespeareApiProvider : IShakespeareApiProvider
    {
        public static readonly string _baseUri = "https://api.funtranslations.com/translate/";

        public static readonly string _shakespeareCommand = "shakespeare.json";

        private IHttpHelper _httpHelper;

        public ShakespeareApiProvider(IHttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public PokemonDescriptionModel GetShakespeareTranslation(string text, string name)
        {
            var payload = new Dictionary<string, object>
            {
                { "text", text}
            };

            var res = _httpHelper.Post(payload, $"{_baseUri}{_shakespeareCommand}").Result;

            if (res == null) throw new NotFoundException($"Translation for pokemon's {name} description could not be retrieved");

            return new PokemonDescriptionModel()
            {
                Name = name,
                Description = res.contents.translated
            };
        }
    }
}