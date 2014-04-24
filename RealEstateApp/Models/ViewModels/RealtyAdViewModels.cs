using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
  public class RealtyAdIndexViewModel
  {
    public RealtyAd RealtyAd { get; set; }
    public ICollection<RealtyAdImage> RealtyAdImages { get; set; }
    public string AddressComplete
    {
      get { return this.RealtyAd.Address + this.RealtyAd.City.Name; }
    }
  }

  public class RealtyAdCreateViewModel
  {
    public int Id { get; set; }
    [Required, StringLength(50, ErrorMessage = "Please limit your title to 50 characters."), Display(Name = "Title")]
    public string ShortDescn { get; set; }

    [Required, StringLength(2500, ErrorMessage = "Please limit the description to 2500 characters."), Display(Name = "Description")]
    public string LongDescn { get; set; }
    [DisplayFormat(DataFormatString = "{0:##,#}")]
    public decimal? Price { get; set; }
    [Display(Name="Category")]
    public RealtyAdCategory Category { get; set; }
    public int Type { get; set; }
    [StringLength(255)]
    public string Address { get; set; }

    [Display(Name = "Bedroom Count")]
    public short BedCount { get; set; }
    [Display(Name = "Bathroom Count")]
    public short BathCount { get; set; }
    [Display(Name = "Floor Area (in sqm)")]
    public float? FloorAreaSqM { get; set; }
    [Display(Name = "Lot Area (in sqm)")]
    public float? LotAreaSqM { get; set; }
    public int City { get; set; }
    
    [Display(Name = "Photos")]
    public IEnumerable<HttpPostedFileBase> PostedImages { get; set; }

  }

  public class RealtyAdDisplaySearchResult
  {
    public int Id { get; set; }
    public string ShortDescn { get; set; }
    [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
    public DateTime DatePosted { get; set; }
    private string _daysAgo;

    public string DaysAgo
    {
      get
      {
        TimeSpan timeSpan = DateTime.Today.Subtract(this.DatePosted.Date);
        return timeSpan.Days.ToString() + " Days";
      }
      set { _daysAgo = value; }
    }
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? Price { get; set; }
    public string Address { get; set; }
    public short BedCount { get; set; }
    public short BathCount { get; set; }
    public float? FloorAreaSqM { get; set; }
    public string ImageUrl {
      get { return String.Format("~/{0}/{1}", RealEstateApp.Helpers.Config.Directories.Images, this.FileName); } 
    }
    public string FileName { get; set; }
  }

  public class RealtyAdItemViewModel
  {
    public int Id { get; set; }
    public string ShortDescn { get; set; }
    [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
    public DateTime DatePosted { get; set; }
    private string _daysAgo;

    public string DaysAgo
    {
      get
      {
        TimeSpan timeSpan = DateTime.Today.Subtract(this.DatePosted.Date);
        return timeSpan.Days.ToString() + " Days";
      }
      set { _daysAgo = value; }
    }
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public decimal? Price { get; set; }
    public string Address { get; set; }
    public short BedCount { get; set; }
    public short BathCount { get; set; }
    public float? FloorAreaSqM { get; set; }

    [Display(Name="Agent")]
    public virtual ApplicationUser ApplicationUser { get; set; }
    public ICollection<RealtyAdImage> RealtyAdImages { get; set; }
  }

}