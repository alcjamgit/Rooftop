using RealEstateApp.DataAccessLayer;
using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.UnitTest.DependencyInjections
{
  public class MockDb:IUnitOfWork
  {

    //public IEnumerable<RealtyAd> RealtyAds { get; set; }
    //public IEnumerable<RealtyAdImage> RealtyAdImages { get; set; }
    //public IEnumerable<RealtyAdImageDefault> RealtyAdImageDefaults { get; set; }
    //public IEnumerable<RealtyAdMessage> RealtyAdMessages { get; set; }
    //public IEnumerable<RealtyAdPageView> RealtyAdPageViews { get; set; }
    //public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
    //public IEnumerable<City> Cities { get; set; }
    public MockDb()
    {
      //_realtyAdRepo = new MockGenericRepo<RealtyAd>(){ {Id =1,}};
      //_realtyAdImageRepo = new MockGenericRepo<RealtyAdImage>(RealtyAdImages);
      //_realtyAdImageDefaultRepo = new MockGenericRepo<RealtyAdImageDefault>(RealtyAdImageDefaults);
      //_realtyAdMessageRepo = new MockGenericRepo<RealtyAdMessage>(RealtyAdMessages);
      //_realtyAdPageViewRepo = new MockGenericRepo<RealtyAdPageView>(RealtyAdPageViews);
      //_applicationUserRepo = new MockGenericRepo<ApplicationUser>(ApplicationUsers);
      //_cityRepo = new MockGenericRepo<City>(Cities);
    }

    #region IUnitOfWork Implementation
    public virtual IGenericRepository<RealtyAd> RealtyAdRepo
    {
      get { return new MockGenericRepo<RealtyAd>(); }
    }

    public IGenericRepository<RealtyAdImage> RealtyAdImageRepo
    {
      get { return new MockGenericRepo<RealtyAdImage>(); }
    }

    public IGenericRepository<RealtyAdImageDefault> RealtyAdImageDefaultRepo
    {
      get { return new MockGenericRepo<RealtyAdImageDefault>(); }
    }

    public IGenericRepository<RealtyAdMessage> RealtyAdMessageRepo
    {
      get { return new MockGenericRepo<RealtyAdMessage>(); }
    }

    public IGenericRepository<RealtyAdPageView> RealtyAdPageViewRepo
    {
      get { return new MockGenericRepo<RealtyAdPageView>(); }
    }

    public IGenericRepository<ApplicationUser> ApplicationUserRepo
    {
      get { return new MockGenericRepo<ApplicationUser>(); }
    }
    public IGenericRepository<City> CityRepo
    {
      get { return new MockGenericRepo<City>(); }
    }

    public void Commit()
    {
      //TODO
    }
    public void Dispose()
    {
      //TODO
    }

    #endregion


  }
}
