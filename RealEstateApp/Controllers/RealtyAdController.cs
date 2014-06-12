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
using System.IO;
using RealEstateApp.Helpers;
using RealEstateApp.ViewModels;
using LinqKit;
using RealEstateApp.DataAccessLayer;

namespace RealEstateApp.Controllers
{
  public class RealtyAdController : Controller
  {
    private IUnitOfWork db;
    public RealtyAdController()
    {
      db = new UnitOfWork();
    }

    //Constructor for dependency injection
    public RealtyAdController(IUnitOfWork unitOfWork)
    {
      db = unitOfWork;
    }

    // GET: /RealtyAd/
    public ActionResult Index()
    {
      ViewBag.TestValue = "123456";
      var realtyads = db.RealtyAdRepo.AsQueryable();
      
      return View(realtyads.ToList());
    }

    // GET: /RealtyAd/Details/5
    public ActionResult Details(int? id)
    {
      var realtyad = db.RealtyAdRepo.Find(r => r.Id == id);

      if (realtyad == null)
      {
        return HttpNotFound();
      }
      return View(realtyad);
    }

    public ActionResult GetProperty(int id)
    {
      RealtyAdDisplayFullViewModel realtyAd = (from r in db.RealtyAdRepo.AsQueryable()
                                               where r.Id == id
                                               select new RealtyAdDisplayFullViewModel()
                                               {
                                                 Id = r.Id,
                                                 Title = r.ShortDescn,
                                                 DatePosted = r.DatePosted,
                                                 Address = r.Address,
                                                 FullDescription = r.LongDescn,
                                                 Price = r.Price,
                                                 BedCount = r.BedCount ?? 0,
                                                 BathCount = r.BathCount ?? 0,
                                                 FloorAreaSqM = r.FloorAreaSqM,
                                                 RealtyAdImages = r.RealtyAdImages,
                                                 Longitude = r.Longitude,
                                                 Latitude = r.Latitude,
                                                 UserId = r.ApplicationUser_Id
                                               }).FirstOrDefault();
      //RealtyAdDisplayFullViewModel realtyAd = db.RealtyAdRepo.Find(r => r.Id == id);

      var realtyAdImages = (from img in db.RealtyAdImageRepo.AsQueryable()
                            where img.RealtyAd_Id == id
                            select img).ToList();


      if (realtyAd==null)
      {
        return HttpNotFound("Sorry no property found with this id.");
      }
      realtyAd.RealtyAdImages = realtyAdImages;
      return View(realtyAd);
    }

    [ChildActionOnly]
    public ActionResult GetAgentProfile(string userId)
    {
      
      var user = (from u in db.ApplicationUserRepo.AsQueryable()
                 where u.Id == userId
                 select new AgentProfile {
                  Id = u.Id,
                  FirstName = u.FirstName,
                  LastName = u.LastName,
                  UserName = u.UserName,
                  Email = u.Email,
                  ProfilePhotoFileName = u.ProfilePhotoFileName,
                  DateJoined = u.DateJoined
                 }).FirstOrDefault();

      return PartialView(user);
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

      var realtyAds = from r in db.RealtyAdRepo.AsQueryable()
                      select new { r.ShortDescn, r.Price, r.Id };
      return Json(realtyAds, JsonRequestBehavior.AllowGet);
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
      //if (searchModel.Location != null)
      //{
      //  //this is the first page since search string is not null
      //  searchModel.Page = 1;
      //}
      //else
      //{
      //  searchModel.Location = searchModel.CurrentSearchFilter;
      //}

      //IEnumerable<RealtyAdDisplayCompactViewModel> realtyAds = from r in db.RealtyAds
      IQueryable<RealtyAdDisplayCompactViewModel> realtyAds = from r in db.RealtyAdRepo.AsQueryable()
                                                              join c in db.CityRepo.AsQueryable() on r.City_Id equals c.Id
                                                              join img in db.RealtyAdImageDefaultRepo.AsQueryable() on r.Id equals img.RealtyAd_Id into outerJoin
                                                              from subjoin in outerJoin.DefaultIfEmpty()
                                                              select new RealtyAdDisplayCompactViewModel
                                                              {
                                                                Id = r.Id,
                                                                ShortDescn = r.ShortDescn,
                                                                City = c.Name + "City",
                                                                Address = r.Address,
                                                                DatePosted = r.DatePosted,
                                                                Price = r.Price,
                                                                FileName = subjoin.FileName ?? "thumbnailPlaceholder400x300.gif",
                                                                BedCount = r.BedCount,
                                                                BathCount = r.BathCount,
                                                                FloorAreaSqM = r.FloorAreaSqM,
                                                                UserId = r.ApplicationUser_Id
                                                              };

      IList<String> locationKeywords;

      var predicate = PredicateBuilder.False<RealtyAdDisplayCompactViewModel>();
      if (!String.IsNullOrEmpty(searchModel.Location))
      {

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
      if ((searchModel.MinPrice ?? 0) > 0)
      {
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

      //count the result for paging
      ViewBag.SearchResultCount = realtyAds.AsExpandable().Where(predicate).Count();
      
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
      
      realtyAds = realtyAds.Skip(searchModel.PagesToSkip ?? 0).Take(searchModel.PageSize ?? 20);
      return realtyAds;
    }

    // GET: /RealtyAd/Create
    [Authorize]
    public ActionResult Create()
    {
      RealtyAdCreateViewModel createModel = new RealtyAdCreateViewModel();
      ViewBag.BedCountSelectItems = GetSelectListFromRange(0,6,suffix:" Beds");
      ViewBag.BathCountSelectItems = GetSelectListFromRange(0, 5, suffix: " Baths");
      ViewBag.FloorAreaSelectItems = GetSelectListFromRange(0, 1000,5,suffix:" sqm.");
      ViewBag.City_Id = new SelectList(db.CityRepo.GetAll(), "Id", "Name");
      return View(createModel);
    }
    // POST: /RealtyAd/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken,Authorize]
    public ActionResult Create(RealtyAdCreateViewModel realtyAdViewModel)
    {
      string userId = User.Identity.GetUserId();
      //use User.Identity.Name for easier unit testing 
      //Mocking User.Identity.GetUserId() would be difficult since Moq cannot do extension methods

      //string userId = User.Identity.Name;
      //string userId = "123";
      if (ModelState.IsValid)
      {        
        RealtyAd realtyAd = new RealtyAd()
        {
          DatePosted = DateTime.Now,
          City_Id = realtyAdViewModel.City ?? 0,
          Price = realtyAdViewModel.Price,
          ShortDescn = realtyAdViewModel.ShortDescn,
          //LongDescn = HttpUtility.HtmlEncode(realtyAdViewModel.LongDescn),
          LongDescn = realtyAdViewModel.LongDescn,
          Type = realtyAdViewModel.Type,
          Category = realtyAdViewModel.Category,
          Address = realtyAdViewModel.Address,
          BedCount = realtyAdViewModel.BedCount,
          BathCount = realtyAdViewModel.BathCount,
          FloorAreaSqM = realtyAdViewModel.FloorAreaSqM,
          Latitude = realtyAdViewModel.Latitude,
          Longitude = realtyAdViewModel.Longitude,
          Status = RealtyAdStatus.Active,
          ApplicationUser_Id = userId
        };

        //save RealtyAd entry
        db.RealtyAdRepo.Add(realtyAd);
        db.Commit();

        ////create user image directory for user
        string userAbsoluteDirectory = Path.Combine(Server.MapPath(string.Format("~/Content/images/{0}", userId)));

        if (!Directory.Exists(userAbsoluteDirectory))
        {
          Directory.CreateDirectory(userAbsoluteDirectory);
        }

        //save Realty Ad Images to the server and add to database
        if (realtyAdViewModel.PostedImages != null)
        {
          foreach (var img in realtyAdViewModel.PostedImages)
          {
            if (img != null)
            {
              string fileExt = Path.GetExtension(img.FileName);
              string fileName = Guid.NewGuid().ToString() + fileExt;
              string relativeDirectory = string.Format("~/Content/images/{0}", userId);
              string absolutePath = Path.Combine(Server.MapPath(relativeDirectory), fileName);
              img.SaveAs(absolutePath);
              db.RealtyAdImageRepo.Add(new RealtyAdImage()
              {
                RealtyAd_Id = realtyAd.Id,
                FileName = fileName
              }
              );
            }
          }
          //save changes for the images
          db.Commit();

          //save Realty Ad Image Default
          var imgSelect = (from i in db.RealtyAdImageRepo.AsQueryable()
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

            db.RealtyAdImageDefaultRepo.Add(realtyAdImgDefault);
            db.Commit();
          }
        }

        return RedirectToAction("Index");
      }

      //model is invalid so reconstruct the original view
      ViewBag.BedCountSelectItems = GetSelectListFromRange(0,6,suffix:" Beds");
      ViewBag.BathCountSelectItems = GetSelectListFromRange(0, 5, suffix: " Baths");
      ViewBag.FloorAreaSelectItems = GetSelectListFromRange(0, 1000,5,suffix:" sqm.");
      ViewBag.City_Id = new SelectList(db.CityRepo.GetAll(), "Id", "Name");
      return View(realtyAdViewModel);
    }

    public IEnumerable<SelectListItem> GetSelectListFromRange(int startVal, int endVal, int increment = 1, string prefix = "", string suffix ="" )
    {
      List<SelectListItem> selItems = new List<SelectListItem>();
      for (int i = startVal; i <= endVal; i+=increment )
      {
        selItems.Add(new SelectListItem() { Text = prefix + i + suffix, Value = i.ToString()});
      }
      return selItems.ToList();
    }

    #region TemporarilyDisabled
    // GET: /RealtyAd/Edit/5
    //public ActionResult Edit(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  RealtyAd realtyad = db.RealtyAds.Find(id);
    //  if (realtyad == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "UserName", realtyad.ApplicationUser_Id);
    //  ViewBag.City_Id = new SelectList(db.Cities, "Id", "Name", realtyad.City_Id);
    //  return View(realtyad);
    //}

    // POST: /RealtyAd/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit([Bind(Include = "Id,ShortDescn,LongDescn,DatePosted,Status,ApplicationUser_Id,Price,Category,Type,Address,FloorAreaSqM,LotAreaSqM,City_Id,Latitude,Longitude")] RealtyAd realtyad)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.Entry(realtyad).State = EntityState.Modified;
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }
    //  ViewBag.ApplicationUser_Id = new SelectList(db.Users, "Id", "UserName", realtyad.ApplicationUser_Id);
    //  ViewBag.City_Id = new SelectList(db.Cities, "Id", "Name", realtyad.City_Id);
    //  return View(realtyad);
    //} 
    #endregion

    // GET: /RealtyAd/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      RealtyAd realtyad = db.RealtyAdRepo.GetById(id??0);
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
      RealtyAd realtyad = db.RealtyAdRepo.GetById(id);
      db.RealtyAdRepo.Delete(realtyad);
      db.Commit();
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
