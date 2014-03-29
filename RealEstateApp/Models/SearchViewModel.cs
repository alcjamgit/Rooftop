using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
  public class SearchViewModel
  {
    public SearchViewModel()
    {
      SearchString = "";
    }
    public int Id { get; set; }
    public string SearchString { get; set; }
    public short? BedCount { get; set; }
    public short? BathCount { get; set; }
    public decimal Price { get; set; }
    public int? Page { get; set; }
    public string CurrentSearchFilter {get;set;}
  }
}