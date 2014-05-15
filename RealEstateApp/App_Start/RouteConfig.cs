using RealEstateApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RealEstateApp
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      //routes.MapRoute(
      //      name: "Search",
      //      url: "search",
      //      defaults: new { controller = "Search", action = "SearchResults"}
      //  );
      routes.MapRoute(
        name: "PropertyDisplay",
        url: "properties/{id}",
        defaults: new { controller = "RealtyAd", action = "GetProperty" },
        constraints: new { id = @"\d+" }
        );

      routes.MapRoute(
        name: "PropertiesForRent",
        url: "properties/for-rent",
        defaults: new { controller = "RealtyAd", action = "DisplaySearchResults"}
      );

      routes.MapRoute(
        name: "PropertiesForSale",
        url: "properties/for-sale",
        defaults: new { controller = "RealtyAd", action = "DisplaySearchResults" }
      );

      routes.MapRoute(
        name: "UserMenu",
        url: "user-menu/{action}",
        defaults: new { controller = "UserMenu", action = "Index" }
      );
      routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );
    }
  }
}
