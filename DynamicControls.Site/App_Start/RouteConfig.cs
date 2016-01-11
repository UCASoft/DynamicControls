﻿using System.Web.Mvc;
using System.Web.Routing;

namespace DynamicControls.Site
{
    /// <summary>
    /// The route config.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// The register routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}", new {controller = "Home", action = "Index", id = UrlParameter.Optional});
        }
    }
}
