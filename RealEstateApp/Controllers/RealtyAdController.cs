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
            var realtyads = db.RealtyAds.Include(r => r.ApplicationUser);
            return View(realtyads.ToList());
        }

        // GET: /RealtyAd/Details/5
        public ActionResult Details(long? id)
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
            ViewBag.ApplicationUserId = new SelectList(db.IdentityUsers, "Id", "UserName");
            return View();
        }

        // POST: /RealtyAd/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ShortDescn,LongDescn,Status,Price,Category,Type,Address,BedCount,BathCount,FloorAreaSqM,LotAreaSqM,CityId,Latitude,Longitude")] RealtyAd realtyad)
        {
            
            realtyad.DatePosted = DateTime.Now;
            realtyad.ApplicationUserId = User.Identity.GetUserId();
            
            if (ModelState.IsValid)
            {
                db.RealtyAds.Add(realtyad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.IdentityUsers, "Id", "UserName", realtyad.ApplicationUserId);
            return View(realtyad);
        }

        // GET: /RealtyAd/Edit/5
        public ActionResult Edit(long? id)
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
            ViewBag.ApplicationUserId = new SelectList(db.IdentityUsers, "Id", "UserName", realtyad.ApplicationUserId);
            return View(realtyad);
        }

        // POST: /RealtyAd/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ShortDescn,LongDescn,DatePosted,Status,ApplicationUserId,Price,Category,Type,Address,BedCount,BathCount,FloorAreaSqM,LotAreaSqM,CityId,Latitude,Longitude")] RealtyAd realtyad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realtyad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.IdentityUsers, "Id", "UserName", realtyad.ApplicationUserId);
            return View(realtyad);
        }

        // GET: /RealtyAd/Delete/5
        public ActionResult Delete(long? id)
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
        public ActionResult DeleteConfirmed(long id)
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
