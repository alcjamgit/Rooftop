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
        public string AddressComplete {
            get { return this.RealtyAd.Address + this.RealtyAd.City.Name; } 
        }
    }

    public class RealtyAdCreateViewModel
    {
        public RealtyAd RealtyAd { get; set; }
        public ICollection<RealtyAdImage> RealtyAdImages { get; set; }
    }


}