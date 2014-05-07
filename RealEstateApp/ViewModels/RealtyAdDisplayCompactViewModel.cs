using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateApp.ViewModels
{
  public class RealtyAdDisplayCompactViewModel
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
    public string City { get; set; }
    public short? BedCount { get; set; }
    public short? BathCount { get; set; }
    public float? FloorAreaSqM { get; set; }
    public string ImageUrl
    {
      get { return String.Format("~/{0}/{1}", RealEstateApp.Helpers.Config.Directories.Images, this.FileName); }
    }
    public string FileName { get; set; }
    public short? PhotoCount { get; set; }

  }
}