using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using RealEstateAppTests.DependencyInjections;
using System.Web;
using RealEstateApp.Models;
using RealEstateApp.ViewModels;
using Moq;
using System.Security.Principal;
using System.Web.Routing;
using System.Security.Claims;

namespace RealEstateApp.Controllers.Tests
{
  [TestClass()]
  public class RealtyAdControllerTests
  {
    [TestMethod()]
    public void Index_Action_Must_Return_Correct_ViewName()
    {
      //Arrange
      var expected = "Index";
      var fakeUnitOfWork = MockDbSetup.SetupInMemoryData();
      var controller = new RealtyAdController(fakeUnitOfWork);

      //Act
      var result = controller.Index() as ViewResult;

      //Assert
      Assert.AreEqual(result.ViewName, expected);
    }

    [TestMethod()]
    public void GetProperty_Action_Must_Return_Correct_ViewName()
    {
      //Arrange
      var expected = "GetProperty";
      var fakeUnitOfWork = MockDbSetup.SetupInMemoryData();
      var controller = new RealtyAdController(fakeUnitOfWork);

      //Act
      var result = controller.GetProperty(2) as ViewResult;

      //Assert
      Assert.AreEqual(result.ViewName, expected);
    }

    [TestMethod()]
    public void Create_Action_Returns_RedirectToRouteResult_When_Model_Is_Valid()
    {
      //Arrange
      var fakeUnitOfWork = MockDbSetup.SetupInMemoryData();

      var model = new RealtyAdCreateViewModel()
      {
        City = 2,
        ShortDescn = "Test Item",
        LongDescn = "Test",
        Category = RealtyAdCategory.Rentals,
        Address = "Some location",
      };

      var context = new Mock<HttpContextBase>();
      var server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
      server.Setup(s => s.MapPath(It.IsAny<string>())).Returns<string>(s => "localhost");
      server.Setup(s => s.MachineName).Returns("localhost");
      context.Setup(s => s.Server).Returns(server.Object);


      var mockIdentity = new Mock<IIdentity>();
      context.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
      mockIdentity.Setup(x => x.Name).Returns("user2");


      var controller = new RealtyAdController(fakeUnitOfWork, server.Object);
      controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

      //Act
      var result = controller.Create(model) as RedirectToRouteResult;

      //Assert
      var action = result.RouteValues["Action"].ToString();
      Assert.AreEqual(action, "Index");
    }

    [TestMethod()]
    public void Create_Action_Returns_RedirectToRouteResult_When_Model_Is_Valid2()
    {
      //Arrange
      var fakeUnitOfWork = MockDbSetup.SetupInMemoryData();

      var model = new RealtyAdCreateViewModel()
      {
        City = 2,
        ShortDescn = "Test Item",
        LongDescn = "Test",
        Category = RealtyAdCategory.Rentals,
        Address = "Some location",
      };

      var context = new Mock<HttpContextBase>();
      var server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
      server.Setup(s => s.MapPath(It.IsAny<string>())).Returns<string>(s => "localhost");
      server.Setup(s => s.MachineName).Returns("localhost");
      context.Setup(s => s.Server).Returns(server.Object);


      var claim = new Claim("user1", "IdOfYourChoosing");
      var mockIdentity = Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);




      var controller = new RealtyAdController(fakeUnitOfWork, server.Object);
      var fakeUser = Mock.Of<IPrincipal>(ip => ip.Identity == mockIdentity);
      controller.HttpContext.User = fakeUser;
      controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

      //Act
      var result = controller.Create(model) as RedirectToRouteResult;

      //Assert
      var action = result.RouteValues["Action"].ToString();
      Assert.AreEqual(action, "Index");
    }

    [TestMethod()]
    public void Create_Action_Returns_RedirectToRouteResult_When_Model_Is_Valid3()
    {
      //Arrange


      var model = new RealtyAdCreateViewModel()
      {
        City = 2,
        ShortDescn = "Test Item",
        LongDescn = "Test",
        Category = RealtyAdCategory.Rentals,
        Address = "Some location",
      };
   
      var fakeUnitOfWork = MockDbSetup.SetupInMemoryData();
      var context = new Mock<ControllerContext>();

      //setup fake user
      context.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("user2");
      
      var controller = new RealtyAdController(fakeUnitOfWork,"user2");
      controller.ControllerContext = context.Object;
      controller.Create(model);

      //Act
      var result = controller.Create(model) as RedirectToRouteResult;

      //Assert
      var action = result.RouteValues["Action"].ToString();
      Assert.AreEqual(action, "Index");

    }

  }
}
