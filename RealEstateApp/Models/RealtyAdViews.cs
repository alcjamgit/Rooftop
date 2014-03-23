using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
    public class RealtyAdView
    {
        public long Id { get; set; }
        public virtual long RealtyAdId {get;set;}
        public DateTime DateViewed { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public virtual RealtyAd RealtyAd { get; set; }
    }
}