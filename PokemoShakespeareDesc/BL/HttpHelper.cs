using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using PokemoShakespeareDesc.Controllers;
using PokemoShakespeareDesc.Interfaces;

namespace PokemonShakespeareDesc.BL
{
    public class HttpHelper : IHttpHelper
    {
        public async Task<dynamic> Post(Dictionary<string, object> payload, string requestUri) 
        {
           
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                using (var response =  client.PostAsync(requestUri, content).Result)
                {
                    return await HandleHttpResponse(response);
                }
            }
        }

        public async Task<dynamic> Get(string requestUri)
        {
           
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(requestUri).Result)
                {
                    return await HandleHttpResponse(response);
                }
            }
        }

       

        private static async Task<dynamic> HandleHttpResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<dynamic>();
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            if ((int)response.StatusCode == 429)
            {
                throw new TooManyRequestsException(
                    "You have submitted too many requests in a short time. There is a limit of 5 requests per hour. Please try again later");
            }


            throw new HttpResponseException(response);
        }

        
    }

    internal class TooManyRequestsException : Exception
    {
        public TooManyRequestsException(string msg) : base(msg)
        {
           
        }
    }

    internal class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}