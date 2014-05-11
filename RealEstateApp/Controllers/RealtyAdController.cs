﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstateApp.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using RealEstateApp.Helpers;
using RealEstateApp.ViewModels;
using LinqKit;
namespace RealEstateApp.Controllers
{
    public class RealtyAdController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /RealtyAd/
        public ActionResult Index()
        {
            var realtyads = db.RealtyAds.Include(r => r.ApplicationUser).Include(r => r.City);
            return View(realtyads.ToList());
        }

        // GET: /RealtyAd/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyAd realtyad = db.RealtyAds.Find(id);
            if (realtyad == null)
            {
                return HttpNotFound();
            }
            return View(realtyad);
        }

        // GET: /RealtyAd/Create
        public ActionResult Create()
        {
          RealtyAdCreateViewModel createModel = new RealtyAdCreateViewModel();
          ViewBag.City_Id = new SelectList(db.Cities, "Id", "Name");
          return View(createModel);
        }

        public ActionResult Search()
        {
          RealtyAdSearchViewModel searchViewModel = new RealtyAdSearchViewModel();
          return View(searchViewModel);
        }
        public ActionResult DisplaySearchResults(RealtyAdSearchViewModel searchModel)
        {
          ViewBag.CurentFilter = searchModel.Location;
          var realtyAds = SearchProperties(searchModel);   
          return View(realtyAds.ToList());
          
        }

        public JsonResult DisplaySearchResultsJson(int bedcount)
        {

          var realtyAds = from r in db.RealtyAds
                          select new { r.ShortDescn, r.Price, r.Id};
          return Json(realtyAds,JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplaySearchResultsPartial(RealtyAdSearchViewModel searchModel)
        {
          //searchModel.PageSize = 3;
          //searchModel.Page = 1;
          ViewBag.CurentFilter = searchModel.Location;
          var realtyAds = SearchProperties(searchModel);
          return PartialView("~/Views/Shared/_DisplaySearchResultsPartial.cshtml", realtyAds.ToList());
          
        }


        private IQueryable<RealtyAdDisplayCompactViewModel> SearchProperties(RealtyAdSearchViewModel searchModel)
        {
          if (searchModel.Location != null)
          {
            //this is the first page since search string is not null
            searchModel.Page = 1;
          }
          else
          {
            searchModel.Location = searchModel.CurrentSearchFilter;
          }
          
          //IEnumerable<RealtyAdDisplayCompactViewModel> realtyAds = from r in db.RealtyAds
          IQueryable<RealtyAdDisplayCompactViewModel> realtyAds = from r in db.RealtyAds
                          join c in db.Cities on r.City_Id equals c.Id
                          join img in db.RealtyAdImageDefaults on r.Id equals img.RealtyAd_Id into outerJoin
                          from subjoin in outerJoin.DefaultIfEmpty()
                          select new RealtyAdDisplayCompactViewModel
                          {
                            Id = r.Id,
                            ShortDescn = r.ShortDescn,
                            City = c.Name + "City",
                            Address = r.Address,
                            DatePosted = r.DatePosted,
                            Price = r.Price,
                            //ImageUrl = "~/Content/Images/" + (subjoin.FileName ?? "thumbnailPlaceholder400x300.gif"),
                            FileName = subjoin.FileName ?? "thumbnailPlaceholder400x300.gif",
                            BedCount = r.BedCount,
                            BathCount = r.BathCount,
                            FloorAreaSqM = r.FloorAreaSqM
                          };

          IList<String> locationKeywords;
          
          var predicate = PredicateBuilder.False<RealtyAdDisplayCompactViewModel>();
          if (!String.IsNullOrEmpty(searchModel.Location)) {
              
              locationKeywords = searchModel.Location.Split().ToList();
              //todo exclude keywords such as avenue, street, village, city, etc.
              //create the unsplit to replace this with blank
              foreach (string keyword in locationKeywords)
              {
                string tempKeyword = keyword;
                predicate = predicate.Or(r => r.City.ToLower().Contains(tempKeyword));
                predicate = predicate.Or(r => r.Address.ToLower().Contains(tempKeyword));
              }

          }
          else
          {
            //need to set to true to return all records
            predicate = PredicateBuilder.True<RealtyAdDisplayCompactViewModel>();
          }

          //var xy = realtyAds.Count();
          if ((searchModel.MinPrice??0) > 0){
            predicate = predicate.And(x => x.Price >= searchModel.MinPrice);
          }

          if ((searchModel.MaxPrice ?? 0) > 0)
          {
            predicate = predicate.And(x => x.Price <= searchModel.MaxPrice);
          }

          if ((searchModel.BedCount ?? 0) > 0)
          {
            predicate = predicate.And(x => x.BedCount >= searchModel.BedCount);
          }

          if ((searchModel.BathCount ?? 0) > 0)
          {
            predicate = predicate.And(x => x.BathCount >= searchModel.BathCount);
          }


          //sort
          switch (searchModel.SortOrder)
          {
            case SortOrder.PostDateDesc:
              realtyAds = realtyAds.AsExpandable().Where(predicate).OrderByDescending(r => r.DatePosted);
              break;
            case SortOrder.CheapestFirst:
              realtyAds = realtyAds.AsExpandable().Where(predicate).OrderBy(r => r.Price);
              break;
            case SortOrder.ExpensiveFirst:
              realtyAds = realtyAds.AsExpandable().Where(predicate).OrderByDescending(r => r.Price);
              break;
            default:
              realtyAds = realtyAds.AsExpandable().Where(predicate).OrderByDescending(r => r.DatePosted);
              break;
          }

          //realtyAds = realtyAds.AsExpandable().Where(predicate);
          var xyz = searchModel.PagesToSkip;
          realtyAds = realtyAds.Skip(searchModel.PagesToSkip??0).Take(searchModel.PageSize??20);
          return realtyAds;
        } 

        // POST: /RealtyAd/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RealtyAdCreateViewModel realtyAdViewModel)
        {
          
          RealtyAd realtyAd = new RealtyAd()
          {
            DatePosted = DateTime.Now,
            City_Id = realtyAdViewModel.City,
            Price = realtyAdViewModel.Price,
            ShortDescn = realtyAdViewModel.ShortDescn,
            LongDescn = realtyAdViewModel.LongDescn,
            Type = realtyAdViewModel.Type,
            Category = realtyAdViewModel.Category,
            Address = realtyAdViewModel.Address,
            BedCount = realtyAdViewModel.BedCount,
            BathCount = realtyAdViewModel.BathCount,
            FloorAreaSqM = realtyAdViewModel.FloorAreaSqM,
            Status = RealtyAdStatus.Active,
            ApplicationUser_Id = User.Identity.GetUserId()
          };

            if (ModelState.IsValid)
            {
              //save RealtyAd entry
              db.RealtyAds.Add(realtyAd);
              db.SaveChanges();

              //save Realty Ad Images to the server and add to database
              foreach(var img in realtyAdViewModel.PostedImages)
              {
                //var fileName = Path.GetFileName(img.FileName);
                var fileExt = Path.GetExtension(img.FileName);
                var fileName = Guid.NewGuid().ToString() +  fileExt;
                var path = Path.Combine(Server.MapPath("~/Content/images"),fileName);
                img.SaveAs(path);
                db.RealtyAdImages.Add(new RealtyAdImage() 
                  { 
                    RealtyAd_Id = realtyAd.Id,
                    FileName = fileName
                  }
                );
              }
              //save changes for the images
              db.SaveChanges();

              //save Realty Ad Image Default
              var imgSelect = (from i in db.RealtyAdImages
                               where i.RealtyAd_Id == realtyAd.Id
                               select i).FirstOrDefault();

              if (imgSelect != null)
              {
                var realtyAdImgDefault = new RealtyAdImageDefault()
                {
                  RealtyAd_Id = imgSelect.RealtyAd_Id,
                  RealtyAdImage_Id = imgSelect.Id,
                  FileName = imgSelect.FileName
                };

                db.RealtyAdImageDefaults.Add(realtyAdImgDefault);
                db.SaveChanges();
              } 
              

              return RedirectToAction("Index");
            }

            return View(realtyAdViewModel);
        }

        // GET: /RealtyAd/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyAd realtyad = db.RealtyAds.Find(id);
            if (realtyad == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "UserName", realtyad.ApplicationUser_Id);
            ViewBag.City_Id = new SelectList(db.Cities, "Id", "Name", realtyad.City_Id);
            return View(realtyad);
        }

        // POST: /RealtyAd/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ShortDescn,LongDescn,DatePosted,Status,ApplicationUser_Id,Price,Category,Type,Address,FloorAreaSqM,LotAreaSqM,City_Id,Latitude,Longitude")] RealtyAd realtyad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realtyad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "UserName", realtyad.ApplicationUser_Id);
            ViewBag.City_Id = new SelectList(db.Cities, "Id", "Name", realtyad.City_Id);
            return View(realtyad);
        }

        // GET: /RealtyAd/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyAd realtyad = db.RealtyAds.Find(id);
            if (realtyad == null)
            {
                return HttpNotFound();
            }
            return View(realtyad);
        }

        // POST: /RealtyAd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RealtyAd realtyad = db.RealtyAds.Find(id);
            db.RealtyAds.Remove(realtyad);
            db.SaveChanges();
            return RedirectToAction("Index");
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
