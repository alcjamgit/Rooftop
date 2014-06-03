using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstateApp.Controllers;

namespace RealEstateApp.UnitTest
{
  [TestClass]
  public class RealtyAdControllerTest
  {

    [TestMethod]
    public void RealtyAdCreate_Should_Not_Be_Valid_When_Model_Is_Invalid()
    {
      var realtyAdController = new RealtyAdController();
    }
  }
}
