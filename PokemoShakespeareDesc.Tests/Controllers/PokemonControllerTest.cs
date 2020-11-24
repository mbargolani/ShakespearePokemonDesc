using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonShakespeareDesc.BL;
using PokemoShakespeareDesc;
using PokemoShakespeareDesc.Controllers;
using PokemoShakespeareDesc.Models;

namespace PokemoShakespeareDesc.Tests.Controllers
{
    [TestClass]
    public class PokemonControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            var helper = new HttpHelper();
            PokemonController controller = new PokemonController(new PokeApiProvider(helper), new ShakespeareApiProvider(helper))
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var result = controller.Get("charizard");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessStatusCode);

            var responseContent = result.Content.ReadAsAsync<PokemonDescriptionModel>().Result;
            
            Assert.IsTrue(responseContent != null);
            Assert.AreEqual(responseContent.Name,"charizard");
            Assert.IsTrue(responseContent.Description.Length > 0);
        }

        [TestMethod]
        public void GetNotExisting()
        { // Arrange
            var helper = new HttpHelper();
            PokemonController controller = new PokemonController(new PokeApiProvider(helper), new ShakespeareApiProvider(helper))
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var result = controller.Get("testing");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessStatusCode);

            var responseContent = result.Content.ReadAsAsync<string>().Result;
            Assert.IsTrue(responseContent != null);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);


        }

        [TestMethod]
        public void TooManyRequests()
        { // Arrange
            var helper = new HttpHelper();
            PokemonController controller = new PokemonController(new PokeApiProvider(helper), new ShakespeareApiProvider(helper))
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var result = controller.Get("testing");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessStatusCode);

            var responseContent = result.Content.ReadAsAsync<string>().Result;
            Assert.IsTrue(responseContent != null);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);


        }


    }
}
