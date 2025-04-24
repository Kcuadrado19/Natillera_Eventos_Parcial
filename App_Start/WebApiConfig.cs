using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Natillera_Eventos_Parcial.Clases;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Natillera_Eventos_Parcial
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //Habilitar el esquema de autenticación, para la validación del token
            config.MessageHandlers.Add(new TokenValidationHandler());
            // Web API routes
            config.MapHttpAttributeRoutes();

            //Habilitar el esquema de autenticación, para la validación del token
            config.MessageHandlers.Add(new TokenValidationHandler());

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
