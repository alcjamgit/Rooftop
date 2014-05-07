﻿using RealEstateApp.Models;
using RealEstateApp.ViewModels;
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

    public ActionResult SearchRealtyAds(string searchString, int? bedCount, string currentFilter, string sortOrder, int? page)
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
                      join c in db.Cities on r.City_Id equals c.Id
                      select new RealtyAdDisplayCompactViewModel  { 
                                                                Id = r.Id,
                                                                ShortDescn = r.ShortDescn,
                                                                Address = r.Address + " " + c.Name + " City",
                                                                DatePosted = r.DatePosted,
                                                                Price = r.Price,
                                                                BedCount = r.BedCount,
                                                                BathCount = r.BathCount
                                                                };

      if (!String.IsNullOrEmpty(searchString))
      {
        //realtyAds = realtyAds.Where(r=>r.ShortDescn.Contains(searchString)).OrderBy(r=>r.DatePosted);
        realtyAds = from r in realtyAds
                    where r.ShortDescn.Contains(searchString)
                    && (bedCount == null || r.BedCount == bedCount)

                    select r;
      }

      int pageSize = 10;
      int pageNumber = (page ?? 1);
      int pagesToSkip = pageSize * pageNumber;
      var i = System.Linq.Enumerable.Count(realtyAds);
      //return View(realtyAds.Skip(pagesToSkip).Take(pageSize).ToList());
      return View(realtyAds.ToList());
    }

    public ActionResult SearchRealtyAdsQueryString()
    {
      var searchString = Request.QueryString["searchString"];
      int bedCount;
      bool isConvertedToInt = int.TryParse(Request.QueryString["bedCount"], out bedCount);
      var currentFilter = "";
      int? page = 0;

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
                      join c in db.Cities on r.City_Id equals c.Id
                      select new RealtyAdDisplayCompactViewModel
                      {
                        Id = r.Id,
                        ShortDescn = r.ShortDescn,
                        Address = r.Address + " " + c.Name + " City",
                        DatePosted = r.DatePosted,
                        Price = r.Price,
                        BedCount = r.BedCount,
                        BathCount = r.BathCount
                      };

      if (!String.IsNullOrEmpty(searchString))
      {
        //realtyAds = realtyAds.Where(r=>r.ShortDescn.Contains(searchString)).OrderBy(r=>r.DatePosted);
        realtyAds = from r in realtyAds
                    where r.ShortDescn.Contains(searchString)
                    select r;
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