using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealEstateApp.DataAccessLayer
{
  public class UnitOfWork: DbContext, IUnitOfWork
  {
    public UnitOfWork()
    {
      _realtyAdRepo = new GenericRepository<RealtyAd>(RealtyAds);
      _realtyAdImageRepo = new GenericRepository<RealtyAdImage>(RealtyAdImages);
      _realtyAdImageDefaultRepo = new GenericRepository<RealtyAdImageDefault>(RealtyAdImageDefaults);
      _realtyAdMessageRepo = new GenericRepository<RealtyAdMessage>(RealtyAdMessages);
      _realtyAdPageViewRepo = new GenericRepository<RealtyAdPageView>(RealtyAdPageViews);
    }

    private readonly GenericRepository<RealtyAd> _realtyAdRepo;
    private readonly GenericRepository<RealtyAdImage> _realtyAdImageRepo;
    private readonly GenericRepository<RealtyAdImageDefault> _realtyAdImageDefaultRepo;
    private readonly GenericRepository<RealtyAdMessage> _realtyAdMessageRepo;
    private readonly GenericRepository<RealtyAdPageView> _realtyAdPageViewRepo;

    public DbSet<RealtyAd> RealtyAds {get;set;}
    public DbSet<RealtyAdImage> RealtyAdImages { get; set; }
    public DbSet<RealtyAdImageDefault> RealtyAdImageDefaults { get; set; }
    public DbSet<RealtyAdMessage> RealtyAdMessages { get; set; }
    public DbSet<RealtyAdPageView> RealtyAdPageViews { get; set; }



    #region IUnitOfWork Implementation
    public IGenericRepository<Models.RealtyAd> RealtyAd
    {
      get { return _realtyAdRepo; }
    }

    public IGenericRepository<Models.RealtyAdImage> RealtyAdImage
    {
      get { return _realtyAdImageRepo; }
    }

    public IGenericRepository<Models.RealtyAdImageDefault> RealtyAdImageDefault
    {
      get { return _realtyAdImageDefaultRepo; }
    }

    public IGenericRepository<Models.RealtyAdMessage> RealtyAdMessage
    {
      get { return _realtyAdMessageRepo; }
    }

    public IGenericRepository<Models.RealtyAdPageView> RealtyAdPageView
    {
      get { return _realtyAdPageViewRepo; }
    }

    public void Commit()
    {
      this.SaveChanges();
    }

    #endregion


  }
}