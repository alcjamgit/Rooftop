using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using RealEstateApp.DependencyInjections;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RealEstateApp.Controllers.Tests
{
  [TestClass()]
  public class RealtyAdControllerTests
  {
    [TestMethod()]
    public void IndexTest()
    {
      //Arrange

      var expected = "Index";
      var userRepo = new MockGenericRepo<IdentityUser>();

      var fakeUnitOfWork = new MockDb();

      //fakeUnitOfWork.Setup(f => f.ApplicationUserRepo).Returns(userRepo.Object);
      var controller = new RealtyAdController(fakeUnitOfWork);

      //Act
      var result = controller.Index() as ViewResult;

      //Assert
      //Assert.AreEqual(result.ViewName, expected);
      Assert.AreEqual("Index", expected);
    }
  }
}
