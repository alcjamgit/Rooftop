using RealEstateApp.DataAccessLayer;
using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.UnitTest.DependencyInjections
{
  class MockDb: IUnitOfWork
  {
    private readonly MockGenericRepo<RealtyAd> _realtyAdRepo;
    private readonly MockGenericRepo<RealtyAdImage> _realtyAdImageRepo;
    private readonly MockGenericRepo<RealtyAdImageDefault> _realtyAdImageDefaultRepo;
    private readonly MockGenericRepo<RealtyAdMessage> _realtyAdMessageRepo;
    private readonly MockGenericRepo<RealtyAdPageView> _realtyAdPageViewRepo;
    private readonly MockGenericRepo<ApplicationUser> _applicationUserRepo;
    private readonly MockGenericRepo<City> _cityRepo;

    public List<RealtyAd> RealtyAds { get; set; }
    public List<RealtyAdImage> RealtyAdImages { get; set; }
    public List<RealtyAdImageDefault> RealtyAdImageDefaults { get; set; }
    public List<RealtyAdMessage> RealtyAdMessages { get; set; }
    public List<RealtyAdPageView> RealtyAdPageViews { get; set; }
    public List<ApplicationUser> ApplicationUsers { get; set; }
    public List<City> Cities { get; set; }
    public MockDb()
    {
      _realtyAdRepo = new MockGenericRepo<RealtyAd>(RealtyAds);
      _realtyAdImageRepo = new MockGenericRepo<RealtyAdImage>(RealtyAdImages);
      _realtyAdImageDefaultRepo = new MockGenericRepo<RealtyAdImageDefault>(RealtyAdImageDefaults);
      _realtyAdMessageRepo = new MockGenericRepo<RealtyAdMessage>(RealtyAdMessages);
      _realtyAdPageViewRepo = new MockGenericRepo<RealtyAdPageView>(RealtyAdPageViews);
      _applicationUserRepo = new MockGenericRepo<ApplicationUser>(ApplicationUsers);
      _cityRepo = new MockGenericRepo<City>(Cities);
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
      
    }

    #endregion
  }
}
