using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateApp.ViewModels
{
  public class RealtyAdCreateViewModel
  {
    public int Id { get; set; }
    [Required, StringLength(50, ErrorMessage = "Please limit your title to 50 characters."), Display(Name = "Title")]
    public string ShortDescn { get; set; }

    [Required, StringLength(2500, ErrorMessage = "Please limit the description to 2500 characters."), Display(Name = "Description")]
    public string LongDescn { get; set; }
    [DisplayFormat(DataFormatString = "{0:##,#}")]
    public decimal? Price { get; set; }
    [Display(Name = "Category")]
    public RealtyAdCategory Category { get; set; }
    public int Type { get; set; }
    [StringLength(255)]
    public string Address { get; set; }

    [Display(Name = "Bedroom Count")]
    public short? BedCount { get; set; }
    [Display(Name = "Bathroom Count")]
    public short? BathCount { get; set; }
    [Display(Name = "Floor Area (sqm)")]
    public float? FloorAreaSqM { get; set; }
    [Display(Name = "Lot Area (in sqm)")]
    public float? LotAreaSqM { get; set; }
    public int City { get; set; }

    [Display(Name = "Photos")]
    public IEnumerable<HttpPostedFileBase> PostedImages { get; set; }
  }
}