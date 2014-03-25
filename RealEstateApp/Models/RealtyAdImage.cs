using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RealEstateApp.Models
{
    public class RealtyAdImage
    {
        
        public long Id { get; set; }
        [ForeignKey("RealtyAd")]
        public virtual int RealtyAd_Id { get; set; }
        [Url]
        public string Url { get; set; }
        public virtual RealtyAd RealtyAd { get; set; }

    }
}
