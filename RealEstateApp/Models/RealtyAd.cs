using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
    public class RealtyAd
    {
        public long Id { get; set; }
        [Required(ErrorMessage="Short Description is required")]
        [Display(Description="Title")]
        [MaxLength(100)]
        public string ShortDescn { get; set; }

        [Display(Name = "Title")]
        public string LongDescn { get; set; }

        [Display(Name="Date Posted")]
        public DateTime DatePosted { get; set; }
        public int Status { get; set; }
        public virtual string ApplicationUserId { get; set; }
        
        [DisplayFormat(DataFormatString="mm/dd/yyyy")]
        public decimal Price { get; set; }
        public int Type { get;set;}
        public string Address { get; set; }
        public int BedCount { get; set; }
        public int BathCount { get; set; }
        public float FloorAreaSqM { get; set; }
        public float LotAreaSqM { get; set; }
        public int CityId { get; set; }
        public float Longitude { get; set; }
        //Navigational Properties
        public virtual City City { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<RealtyAdMessage> RealtyAdMessages { get; set; }
        public virtual ICollection<RealtyAdView> Views { get; set; }
    }
}