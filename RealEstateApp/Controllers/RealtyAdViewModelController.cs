using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateApp.Controllers
{
  public class RealtyAdViewModelController : Controller
  {
    public ApplicationDbContext db = new ApplicationDbContext();
    //
    // GET: /RealtyAdViewModel/
    public ActionResult Index()
    {

      return View();
    }

    public ActionResult SearchRealtyAds(string searchString, string currentFilter, string sortOrder, int? page)
    {
      if (searchString != null)
      {
        //this is the first page since search string is not null
        page = 1;
      }
      else
      {
        searchString = currentFilter;
      }

      ViewBag.CurentFilter = searchString;

      var realtyAds = from r in db.RealtyAds
                      select r;

      if (!String.IsNullOrEmpty(searchString))
      {
        realtyAds = realtyAds.Where(r=>r.ShortDescn.Contains(searchString)).OrderBy(r=>r.DatePosted);
      }

      int pageSize = 10;
      int pageNumber = (page ?? 1);
      int pagesToSkip = pageSize * pageNumber;
      var i = System.Linq.Enumerable.Count(realtyAds);
      //return View(realtyAds.Skip(pagesToSkip).Take(pageSize).ToList());
      return View(realtyAds.ToList());
    }
  }
}