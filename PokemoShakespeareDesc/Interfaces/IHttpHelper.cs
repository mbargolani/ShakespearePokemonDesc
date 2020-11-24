using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PokemoShakespeareDesc.Interfaces
{
    /// <summary>
    /// An interface that declares the Post and Get methods vs external APIs
    /// </summary>
    public interface IHttpHelper
    {
        /// <summary>
        /// Calls HttpClient with Post command
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
       Task<dynamic> Post(Dictionary<string,object> payload, string uri);

        /// <summary>
        /// Calls HttpClient with Get command
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
       Task<dynamic> Get(string uri);

    }
}