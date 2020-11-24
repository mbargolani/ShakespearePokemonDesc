using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using PokemonShakespeareDesc.BL;
using PokemoShakespeareDesc.Controllers;
using PokemoShakespeareDesc.Interfaces;
using Unity.Mvc4;

namespace PokemoShakespeareDesc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static class UnityConfig
        {
            public static IUnityContainer GetConfiguredContainer()
            {
                var container = new UnityContainer();

                container.RegisterType<IHttpHelper, HttpHelper>();
                container.RegisterType<IPokeApiProvider, PokeApiProvider>();
                container.RegisterType<IShakespeareApiProvider, ShakespeareApiProvider>();
                container.RegisterType<IHttpController, PokemonController>("Pokemon");

                
                
               
                return container;
            }
        }

        public class UnityResolver : IDependencyResolver
        {
            protected IUnityContainer container;

            public UnityResolver(IUnityContainer container)
            {
                if (container == null)
                {
                    throw new ArgumentNullException("container");
                }
                this.container = container;
            }

            public object GetService(Type serviceType)
            {
                try
                {
                    return container.Resolve(serviceType);
                }
                catch (ResolutionFailedException)
                {
                    return null;
                }
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                try
                {
                    return container.ResolveAll(serviceType);
                }
                catch (ResolutionFailedException)
                {
                    return new List<object>();
                }
            }

            public IDependencyScope BeginScope()
            {
                var child = container.CreateChildContainer();
                return new UnityResolver(child);
            }

            public void Dispose()
            {
                container.Dispose();
            }
        }
    }
}
