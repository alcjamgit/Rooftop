using Microsoft.AspNet.Identity;
using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateApp.DataAccessLayer
{
  public interface IUnitOfWork:IDisposable
  {
    IGenericRepository<RealtyAd> RealtyAdRepo { get; }
    IGenericRepository<RealtyAdImage> RealtyAdImageRepo { get; }
    IGenericRepository<RealtyAdImageDefault> RealtyAdImageDefaultRepo { get; }
    IGenericRepository<RealtyAdMessage> RealtyAdMessageRepo { get; }
    IGenericRepository<RealtyAdPageView> RealtyAdPageViewRepo { get; }
    IGenericRepository<ApplicationUser> ApplicationUserRepo { get; }
    IGenericRepository<City> CityRepo { get; }
    void Commit();
  }
}