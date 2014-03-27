using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
    public enum RealtyAdCategory
    {
        [Description("For Sale")]
        Homes = 1,
        [Description("For Rent")]
        Rentals = 2,
        [Description("Commercial Space")]
        Commercial = 4,    
    }

    public enum RealtyAdStatus
    {
        Active = 1,
        Inactive = 2,
    }
    public enum RealtyAdType
    {
        Apartment = 1,
        Condo = 2,
    }
    public class RealtyAd
    {
        public int Id { get; set; }
      
        [Required, StringLength(50, ErrorMessage = "Please limit your title to 50 characters."), Display(Name = "Title")]
        public string ShortDescn { get; set; }
        [Display(Name="Description")]
        public string LongDescn { get; set; }

        [DataType(DataType.Date), Display(Name="Date Posted"),DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePosted { get; set; }

        [DefaultValue(RealtyAdStatus.Active)]
        public RealtyAdStatus Status { get; set; }

        [ForeignKey("ApplicationUser")]
        public virtual string ApplicationUser_Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:##,#}")]
        public decimal? Price { get; set; }
        public RealtyAdCategory Category { get;set;}
        public int Type { get;set;}
        [StringLength(255)]
        public string Address { get; set; }
        public short BedCount { get; set; }
        public short BathCount { get; set; }
        public float? FloorAreaSqM { get; set; }
        public float? LotAreaSqM { get; set; }

        [ForeignKey("City")]
        public virtual int City_Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        //Navigational Properties
        public virtual City City { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<RealtyAdMessage> RealtyAdMessages { get; set; }
        public virtual ICollection<RealtyAdPageView> Views { get; set; }
        public virtual ICollection<RealtyAdImage> RealtyAdImages { get; set; }
    }
}