using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateApp.ViewModels
{
  public enum SortOrder
  {
    [Display(Description="Relevance")]
    Relevance = 0,
    [Display(Description = "Post Date")]
    PostDateDesc = 1,
    [Display(Description = "Cheapest First")]
    CheapestFirst = 2,
    [Display(Description = "Expensive First")]
    ExpensiveFirst =4,
  }
  public class RealtyAdSearchViewModel
  {
    public RealtyAdSearchViewModel()
    {
      Location = "";
      Page = 1;
      PageSize = 20;
      SortOrder = SortOrder.Relevance;
    }

    public string Location { get; set; }
    public short? BedCount { get; set; }
    public short? BathCount { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string CurrentSearchFilter {get;set;}
    public int? PagesToSkip {
      get {
        var temp = (this.Page-1) * this.PageSize;
        return temp;
      }
    }
    public SortOrder SortOrder { get; set; }
  }
}