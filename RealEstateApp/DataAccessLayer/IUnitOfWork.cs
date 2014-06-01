using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateApp.DataAccessLayer
{
  public interface IUnitOfWork:IDisposable
  {
    IGenericRepository<RealtyAd> RealtyAd { get; }
    IGenericRepository<RealtyAdImage> RealtyAdImage { get; }
    IGenericRepository<RealtyAdImageDefault> RealtyAdImageDefault { get; }
    IGenericRepository<RealtyAdMessage> RealtyAdMessage { get; }
    IGenericRepository<RealtyAdPageView> RealtyAdPageView { get; }
    void Commit();
  }
}