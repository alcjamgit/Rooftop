﻿using System;
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
    [Required, StringLength(50, ErrorMessage = "Please limit your title to 50 characters."), Display(Name = "Title")]
    public string ShortDescn { get; set; }
    [Display(Name = "Description")]
    public string LongDescn { get; set; }
    [DisplayFormat(DataFormatString = "{0:##,#}")]
    public decimal? Price { get; set; }
    public RealtyAdCategory Category { get; set; }
    public int Type { get; set; }
    [StringLength(255)]
    public string Address { get; set; }
    public short BedCount { get; set; }
    public short BathCount { get; set; }
    public float? FloorAreaSqM { get; set; }
    public float? LotAreaSqM { get; set; }
    public virtual City City { get; set; }
    public ICollection<RealtyAdImage> RealtyAdImages { get; set; }

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
    public string ImageUrl { get; set; }
    public float FloorAreaSqM { get; set; }

  }


}