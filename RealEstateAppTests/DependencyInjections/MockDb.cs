using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RealEstateApp.DataAccessLayer;
using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAppTests.DependencyInjections
{
  public class MockDb : IUnitOfWork
  {
    private IGenericRepository<RealtyAd> _realtyAdRepo;
    private IGenericRepository<RealtyAdImage> _realtyAdImageRepo;
    private IGenericRepository<RealtyAdImageDefault> _realtyAdImageDefaultRepo;
    private IGenericRepository<RealtyAdMessage> _realtyAdMessageRepo;
    private IGenericRepository<RealtyAdPageView> _realtyAdPageViewRepo;
    private IGenericRepository<ApplicationUser> _applicationUserRepo;
    private IGenericRepository<City> _cityRepo;

    //private bool _disposed = false;

    public MockDb()
    {
      _realtyAdRepo = new MockGenericRepo<RealtyAd>();
      _realtyAdImageRepo = new MockGenericRepo<RealtyAdImage>();
      _realtyAdImageDefaultRepo = new MockGenericRepo<RealtyAdImageDefault>();
      _realtyAdMessageRepo = new MockGenericRepo<RealtyAdMessage>();
      _realtyAdPageViewRepo = new MockGenericRepo<RealtyAdPageView>();
      _applicationUserRepo = new MockGenericRepo<ApplicationUser>();
      _cityRepo = new MockGenericRepo<City>();
    }

    #region IUnitOfWork Implementation

    //Properties set to virtual so that mocking frameworks can override behavior
    public virtual IGenericRepository<RealtyAd> RealtyAdRepo
    {
      get
      {
        return _realtyAdRepo;
      }
      set { _realtyAdRepo = value; }
    }

    public virtual IGenericRepository<RealtyAdImage> RealtyAdImageRepo
    {
      get { return _realtyAdImageRepo; }
      set { _realtyAdImageRepo = value; }
    }

    public virtual IGenericRepository<RealtyAdImageDefault> RealtyAdImageDefaultRepo
    {
      get { return _realtyAdImageDefaultRepo; }
      set { _realtyAdImageDefaultRepo = value; }
    }

    public virtual IGenericRepository<RealtyAdMessage> RealtyAdMessageRepo
    {
      get { return _realtyAdMessageRepo; }
      set { _realtyAdMessageRepo = value; }
    }

    public virtual IGenericRepository<RealtyAdPageView> RealtyAdPageViewRepo
    {
      get { return _realtyAdPageViewRepo; }
      set { _realtyAdPageViewRepo = value; }
    }

    public virtual IGenericRepository<ApplicationUser> ApplicationUserRepo
    {

      get
      {
        //UserStore<ApplicationUser> test = new UserStore<ApplicationUser>();
        List<ApplicationUser> appUser = new List<ApplicationUser>();
        appUser.Add(new ApplicationUser() { Id = "user1", UserName = "JohnDoe", Email = "JohnDoe@gmail.com", PasswordHash = "TopSecret1" });
        appUser.Add(new ApplicationUser() { Id = "user2", UserName = "JaneDoe", Email = "JaneDoe@gmail.com", PasswordHash = "TopSecret1" });
        //return new MockGenericRepo<ApplicationUser>(appUser);
        return _applicationUserRepo;
      }
      set { _applicationUserRepo = value; }
    }

    public virtual IGenericRepository<City> CityRepo
    {
      get { return _cityRepo; }
      set { _cityRepo = value; }
    }

    public void Commit()
    {
      //TODO
    }

    #region Dispose code commented out
    //protected virtual void Dispose(bool disposing)
    //{
    //  if (!_disposed)
    //  {
    //    if (disposing)
    //    {
    //      // dispose-only, i.e. non-finalizable logic
    //    }

    //    // shared cleanup logic
    //    _disposed = true;
    //  }
    //} 
    #endregion

    public void Dispose()
    {
      //Dispose(true);
    }

    #endregion




  }
}