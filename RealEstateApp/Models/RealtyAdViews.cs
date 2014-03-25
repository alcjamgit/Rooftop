using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
    public class RealtyAdView
    {
        public long Id { get; set; }
        [ForeignKey("RealtyAd")]
        public virtual int RealtyAd_Id {get;set;}
        [DataType(DataType.Date), Display(Name = "Date Viewed"), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateViewed { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public virtual RealtyAd RealtyAd { get; set; }
    }
}