using Microsoft.AspNet.Identity.EntityFramework;
using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealEstateApp.DataAccessLayer
{
  public class UnitOfWork : IdentityDbContext, IUnitOfWork
  {
    private readonly GenericRepository<RealtyAd> _realtyAdRepo;
    private readonly GenericRepository<RealtyAdImage> _realtyAdImageRepo;
    private readonly GenericRepository<RealtyAdImageDefault> _realtyAdImageDefaultRepo;
    private readonly GenericRepository<RealtyAdMessage> _realtyAdMessageRepo;
    private readonly GenericRepository<RealtyAdPageView> _realtyAdPageViewRepo;
    private readonly GenericRepository<ApplicationUser> _applicationUserRepo;
    private readonly GenericRepository<City> _cityRepo;

    public DbSet<RealtyAd> RealtyAds { get; set; }
    public DbSet<RealtyAdImage> RealtyAdImages { get; set; }
    public DbSet<RealtyAdImageDefault> RealtyAdImageDefaults { get; set; }
    public DbSet<RealtyAdMessage> RealtyAdMessages { get; set; }
    public DbSet<RealtyAdPageView> RealtyAdPageViews { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<City> Cities { get; set; }
    public UnitOfWork()
    {
      _realtyAdRepo = new GenericRepository<RealtyAd>(RealtyAds);
      _realtyAdImageRepo = new GenericRepository<RealtyAdImage>(RealtyAdImages);
      _realtyAdImageDefaultRepo = new GenericRepository<RealtyAdImageDefault>(RealtyAdImageDefaults);
      _realtyAdMessageRepo = new GenericRepository<RealtyAdMessage>(RealtyAdMessages);
      _realtyAdPageViewRepo = new GenericRepository<RealtyAdPageView>(RealtyAdPageViews);
      _applicationUserRepo = new GenericRepository<ApplicationUser>(ApplicationUsers);
      _cityRepo = new GenericRepository<City>(Cities);
    }

    #region IUnitOfWork Implementation
    public IGenericRepository<RealtyAd> RealtyAdRepo
    {
      get { return _realtyAdRepo; }
    }

    public IGenericRepository<RealtyAdImage> RealtyAdImageRepo
    {
      get { return _realtyAdImageRepo; }
    }

    public IGenericRepository<RealtyAdImageDefault> RealtyAdImageDefaultRepo
    {
      get { return _realtyAdImageDefaultRepo; }
    }

    public IGenericRepository<RealtyAdMessage> RealtyAdMessageRepo
    {
      get { return _realtyAdMessageRepo; }
    }

    public IGenericRepository<RealtyAdPageView> RealtyAdPageViewRepo
    {
      get { return _realtyAdPageViewRepo; }
    }

    public IGenericRepository<ApplicationUser> ApplicationUserRepo
    {
      get { return _applicationUserRepo; }
    }
    public IGenericRepository<City> CityRepo
    {
      get { return _cityRepo; }
    }

    public void Commit()
    {
      this.SaveChanges();
    }

    #endregion
  }
}