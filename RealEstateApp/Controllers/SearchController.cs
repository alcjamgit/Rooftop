using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstateApp.Models;

namespace RealEstateApp.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Search/
        public ActionResult Index()
        {
            return View(db.RealtyAds.ToList());
        }

        // GET: /Search/Details/5


        
        public ActionResult Search()
        {

          SearchViewModel searchViewModel = new SearchViewModel();
          return View(searchViewModel);
        }

        // POST: /Search/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [ValidateAntiForgeryToken]
        public ActionResult SearchResults([Bind(Include="SearchString,BedCount,BathCount,Price")] SearchViewModel searchModel)
        {
          if (searchModel.SearchString != null)
          {
            //this is the first page since search string is not null
            searchModel.Page = 1;
          }
          else
          {
            searchModel.SearchString = searchModel.CurrentSearchFilter;
          }

          ViewBag.CurentFilter = searchModel.SearchString;

          var realtyAds = from r in db.RealtyAds
                          join c in db.Cities on r.City_Id equals c.Id
                          select new RealtyAdDisplaySearchResult
                          {
                            Id = r.Id,
                            ShortDescn = r.ShortDescn,
                            Address = r.Address + " " + c.Name + " City",
                            DatePosted = r.DatePosted,
                            Price = r.Price,
                            ImageUrl = "",
                            BedCount = r.BedCount,
                            BathCount = r.BathCount
                          };

          if (!String.IsNullOrEmpty(searchModel.SearchString))
          {
            //realtyAds = realtyAds.Where(r=>r.ShortDescn.Contains(searchString)).OrderBy(r=>r.DatePosted);
            realtyAds = from r in realtyAds
                        where r.ShortDescn.Contains(searchModel.SearchString)
                        && (searchModel.BedCount == null || r.BedCount == searchModel.BedCount)
                        select r;
          }

          int pageSize = 10;
          int pageNumber = (searchModel.Page ?? 1);
          int pagesToSkip = pageSize * pageNumber;
          var i = System.Linq.Enumerable.Count(realtyAds);
          //return View(realtyAds.Skip(pagesToSkip).Take(pageSize).ToList());
          return View(realtyAds.ToList());

        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
