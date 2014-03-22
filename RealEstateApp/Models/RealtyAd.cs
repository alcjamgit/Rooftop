using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
    public class RealtyAd
    {
        public long Id { get; set; }
        public string ShortDescn { get; set; }
        public string LongDescn { get; set; }
        public DateTime DatePosted { get; set; }
        //public virtual string UserId { get; set; }
        public int Status { get; set; }
        public long UserId { get; set; }
        public decimal Price { get; set; }
        public int Type { get;set;}
        public string Address { get; set; }
        public int BedCount { get; set; }
        public int BathCount { get; set; }
        public float FloorAreaSqM { get; set; }
        public float LotAreaSqM { get; set; }
        public long CityId { get; set; }
        
        //Navigational Properties
        public virtual City City { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}