using System;
using System.Data.Entity;
namespace RealEstateApp.Models
{
  interface IApplicationDbContext
  {
    IDbSet<City> Cities { get; set; }
    IDbSet<RealtyAdImageDefault> RealtyAdImageDefaults { get; set; }
    IDbSet<RealtyAdImage> RealtyAdImages { get; set; }
    IDbSet<RealtyAdMessage> RealtyAdMessages { get; set; }
    IDbSet<RealtyAd> RealtyAds { get; set; }
    IDbSet<RealtyAdPageView> RealtyAdViews { get; set; }
  }
}
