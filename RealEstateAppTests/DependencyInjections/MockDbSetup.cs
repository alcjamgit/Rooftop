using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAppTests.DependencyInjections
{
  public static class MockDbSetup
  {
    public static MockDb SetupInMemoryData()
    {
      var db = new MockDb();
      db.RealtyAdRepo = SetupRealtyAds();
      db.ApplicationUserRepo = SetupApplicationUsers();
      db.CityRepo = SetupCities();
      return db;
    }

    public static MockGenericRepo<RealtyAd> SetupRealtyAds()
    {
      var realtyAds = new List<RealtyAd>();
      RealtyAd realtyAd;
      for (int i = 1; i <= 10; i++)
      {
        realtyAd = new RealtyAd()
        {
          Id = i,
          ShortDescn = string.Format("Test Data {0}", i),
          City_Id = i,
          BedCount = 1,
          BathCount = 1,
          DatePosted = new DateTime(2014, 7, 12),
          ApplicationUser_Id = string.Format("user{0}", i),
          Address = string.Format("#{0} Silangan Street", i),
          Status = RealtyAdStatus.Active,
          Price = 4500 + i,
          Category = RealtyAdCategory.Rentals
        };
        realtyAds.Add(realtyAd);
      }
      return new MockGenericRepo<RealtyAd>(realtyAds);
    }
    public static MockGenericRepo<ApplicationUser> SetupApplicationUsers()
    {
      var users = new List<ApplicationUser>();
      ApplicationUser user;
      for (int i = 1; i <= 10; i++)
      {
        user = new ApplicationUser()
        {
          Id = string.Format("user{0}", i),
          FirstName = "John",
          LastName = "Doe",
          Email = "JohnDoe@gmail.com",
          DateJoined = new DateTime(2010, 7, 12),
          CellphoneNum = "+639178932123",
        };
        users.Add(user);
      }
      return new MockGenericRepo<ApplicationUser>(users);
    }
    public static MockGenericRepo<City> SetupCities()
    {
      var cities = new List<City>();
      City city;
      for (int i = 1; i <= 13; i++)
      {
        city = new City()
        {
          Id = i,
          Name = "City" + i,
        };
        cities.Add(city);
      }
      return new MockGenericRepo<City>(cities);
    }

    public static MockGenericRepo<RealtyAdImage> RealtyAdImages()
    {
      var images = new List<RealtyAdImage>();
      RealtyAdImage image;
      for (int i = 1; i <= 13; i++)
      {
        image = new RealtyAdImage()
        {
          Id = i,
          FileName = "400x300.jpg",
          Caption = "Kitchen"
        };
        images.Add(image);
      }
      return new MockGenericRepo<RealtyAdImage>(images);
    }
  }
}
