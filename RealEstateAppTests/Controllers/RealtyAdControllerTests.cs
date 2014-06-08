using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstateApp.UnitTest.DependencyInjections;
using RealEstateApp.Models;
using System.Web.Mvc;
using RealEstateApp.ViewModels;
using Moq;
using RealEstateApp.DataAccessLayer;
using System.Web;

//using System.Web.HttpContextBase;

namespace RealEstateApp.Controllers.Tests
{
  [TestClass()]
  public class RealtyAdControllerTests
  {
    [TestMethod()]
    public void DetailsMustReturnView()
    {
      //Arrange
      RealtyAdController controller = new RealtyAdController(new MockDb());

      //Act
      ViewResult result = (ViewResult)controller.Details(3);
      var x = result.ViewName;
      //Assert
      Assert.IsNotNull(controller);
    }

    [TestMethod()]
    public void DeleteRealtyAd()
    {
      //Arrange
      var db = new MockDb();
      RealtyAdController controller = new RealtyAdController(db);
      
      #region add repo
      //var realtyAd = new RealtyAd
      //  {
      //    Id = 3,
      //    ShortDescn = "Test",
      //    City_Id = 10
      //  }
      //;

      //db.RealtyAdRepo.Add(new RealtyAd()); 
      #endregion

      //Act
      var result = controller.Delete(0);

      //Assert
      Assert.IsNotNull(controller);
    }

    [TestMethod()]
    public void Details_Should_Return_Not_Found_View()
    {
      //TODO: mock application user
      //Arrange
      var db = new MockDb();
      var realtyAd = new RealtyAd
      {
        Id = 3,
        ShortDescn = "Test",
        City_Id = 10,
        ApplicationUser_Id = "user123",
      };
      var city = new City { Id = 10, Name = "Makati" };
      //var user = new ApplicationUser
      //{
      //  Id = "user123",
      //  UserName = "JohnDoe",
      //  PasswordHash = "mdsxsx",
      //  Email = "alcjame@yahoo.com"
      //};
      //db.ApplicationUserRepo.Add(user);
      db.RealtyAdRepo.Add(realtyAd);
      db.CityRepo.Add(city);
      
      
      //Act
      RealtyAdController controller = new RealtyAdController(db);
      var result = controller.Details(0) as ViewResult;      
      
      //Assert
      Assert.AreEqual("NotFound", result.ViewName);
    }

    [TestMethod()]
    public void CreateTest()
    {
      RealtyAdCreateViewModel vm = new RealtyAdCreateViewModel() 
      { 
        Id=1,
        ShortDescn = "Test Item",
        Type = 2,
        Category = RealtyAdCategory.Rentals,
        Price = 45555,
        City = 13,
        Address = "123 Makati"
      };

      //Mock getUserId
      
      //var context = new Mock<HttpContextBase>();
      var db = new Mock<MockDb>();
      var realtyRepo = new Mock<MockGenericRepo<RealtyAd>>();
      db.Setup(r => r.RealtyAdRepo).Returns(realtyRepo.Object);
      //HttpContextBase httpContext;
      RealtyAdController controller = new RealtyAdController(db.Object);
      controller.Create(vm);
      //System.Web.HttpContextBase context;
      
      Assert.AreEqual(controller.ModelState.IsValid,true);

    }

    [TestMethod()]
    public void IndexTest()
    {
      var expected = "Index";
      var fakeUnitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
      var controller = new RealtyAdController(fakeUnitOfWork.Object);

      var result = controller.Index() as ViewResult;
      
      //Assert.IsNotNull(result);
      Assert.AreEqual(result.ViewName, expected);
    }



  }
}
