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
    public class UserViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /UserViewModels/
        public ActionResult Index()
        {
            return View(db.IdentityUsers.ToList());
        }

        // GET: /UserViewModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationuser = db.IdentityUsers.Find(id);
            if (applicationuser == null)
            {
                return HttpNotFound();
            }
            return View(applicationuser);
        }

        // GET: /UserViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /UserViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,UserName,PasswordHash,SecurityStamp,Email,FirstName,LastName,CellphoneNum,TelephoneNum,IsRealtyAgent,AboutMessage")] ApplicationUser applicationuser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationuser);
        }

        // GET: /UserViewModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationuser = db.IdentityUsers.Find(id);
            var updateUserProfile = new UpdateUserProfileViewModel
            {

              FirstName = applicationuser.FirstName,
              LastName = applicationuser.LastName,
              CellphoneNum = applicationuser.CellphoneNum,
              IsRealtyAgent = applicationuser.IsRealtyAgent
            };
            if (applicationuser == null)
            {
                return HttpNotFound();
            }
            return View(updateUserProfile);
        }

        // POST: /UserViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,UserName,PasswordHash,SecurityStamp,Email,FirstName,LastName,CellphoneNum,TelephoneNum,IsRealtyAgent,AboutMessage")] ApplicationUser applicationuser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationuser);
        }

        // GET: /UserViewModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationuser = db.IdentityUsers.Find(id);
            if (applicationuser == null)
            {
                return HttpNotFound();
            }
            return View(applicationuser);
        }

        // POST: /UserViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationuser = db.IdentityUsers.Find(id);
            db.Users.Remove(applicationuser);
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
