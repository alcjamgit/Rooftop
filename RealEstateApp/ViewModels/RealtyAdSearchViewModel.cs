using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateApp.ViewModels
{
  public class RealtyAdSearchViewModel
  {
    public RealtyAdSearchViewModel()
    {
      Location = "";
    }

    public string Location { get; set; }
    public short? BedCount { get; set; }
    public short? BathCount { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public int? Page { get; set; }
    public string CurrentSearchFilter {get;set;}
  }
}