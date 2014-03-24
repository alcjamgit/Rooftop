using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace RealEstateApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(30)]
        public string CellphoneNum { get; set; }
        public string TelephoneNum { get; set; }
        public bool IsRealtyAgent { get; set; }
        public string AboutMessage { get; set; }
        public virtual ICollection<RealtyAd> Ads { get; set; }
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<RealtyAd> RealtyAds { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<RealtyAdMessage> RealtyAdMessages { get; set; }
        public DbSet<RealtyAdView> RealtyAdViews { get; set; }
        public DbSet<RealtyAdImage> RealtyAdImages { get; set; }

        public System.Data.Entity.DbSet<RealEstateApp.Models.ApplicationUser> IdentityUsers { get; set; }

        //TODO:Check redundancy of adding this to t
        //public DbSet<ApplicationUser> IdentityUsers { get; set; }
        

    }
}