using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstateApp.Models;
using RealEstateApp.Helpers;
using System.IO;
using RealEstateApp.ViewModels;

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

          RealtyAdSearchViewModel searchViewModel = new RealtyAdSearchViewModel();
          return View(searchViewModel);
        }

        // POST: /Search/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        



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
