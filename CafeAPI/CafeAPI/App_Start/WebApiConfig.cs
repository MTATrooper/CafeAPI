﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CafeAPI
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
            //config.Formatters.XmlFormatter.UseXmlSerializer = true;
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling =
            //Newtonsoft.Json.PreserveReferencesHandling.Objects;
        }
    }
}
