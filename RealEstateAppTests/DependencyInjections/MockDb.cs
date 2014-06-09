using Microsoft.AspNet.Identity.EntityFramework;
using RealEstateApp.DataAccessLayer;
using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.UnitTest.DependencyInjections
{
  public class MockDb : IdentityDbContext<ApplicationUser>, IUnitOfWork
  {

    #region IUnitOfWork Implementation

    //Properties set to virtual so that mocking frameworks can override behavior
    public virtual IGenericRepository<RealtyAd> RealtyAdRepo
    {
      get { return new MockGenericRepo<RealtyAd>(); }
    }

    public virtual IGenericRepository<RealtyAdImage> RealtyAdImageRepo
    {
      get { return new MockGenericRepo<RealtyAdImage>(); }
    }

    public virtual IGenericRepository<RealtyAdImageDefault> RealtyAdImageDefaultRepo
    {
      get { return new MockGenericRepo<RealtyAdImageDefault>(); }
    }

    public virtual IGenericRepository<RealtyAdMessage> RealtyAdMessageRepo
    {
      get { return new MockGenericRepo<RealtyAdMessage>(); }
    }

    public virtual IGenericRepository<RealtyAdPageView> RealtyAdPageViewRepo
    {
      get { return new MockGenericRepo<RealtyAdPageView>(); }
    }

    public virtual IGenericRepository<ApplicationUser> ApplicationUserRepo
    {
      
      get 
      {
        List<ApplicationUser> appUser = new List<ApplicationUser>();
        appUser.Add(new ApplicationUser() { Id = "user1",UserName = "JohnDoe",Email = "JohnDoe@gmail.com",PasswordHash ="TopSecret1" } );
        appUser.Add(new ApplicationUser() { Id = "user2", UserName = "JaneDoe", Email = "JaneDoe@gmail.com", PasswordHash = "TopSecret1" });
        return new MockGenericRepo<ApplicationUser>(appUser); 
      }
    }
    public virtual IGenericRepository<City> CityRepo
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
