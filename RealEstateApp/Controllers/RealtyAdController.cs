using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstateApp.Models;
using Microsoft.AspNet.Identity;
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
            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "UserName");
            ViewBag.City_Id = new SelectList(db.Cities, "Id", "Name");
            return View();
        }

        // POST: /RealtyAd/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=@"Id,ShortDescn,LongDescn,Status,BedCount,BathCount, 
                Price,Category,Type,Address,FloorAreaSqM,LotAreaSqM,City_Id,Latitude,Longitude")] RealtyAd realtyad)
        {
            realtyad.DatePosted = DateTime.Now;
            realtyad.ApplicationUser_Id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.RealtyAds.Add(realtyad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "UserName", realtyad.ApplicationUser_Id);
            ViewBag.City_Id = new SelectList(db.Cities, "Id", "Name", realtyad.City_Id);
            return View(realtyad);
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
