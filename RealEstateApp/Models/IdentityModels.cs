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
        [Required, StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(50), Display(Name="First Name")]
        public string FirstName { get; set; }
        [StringLength(50), Display(Name = "Last Name")]
        public string LastName { get; set; }
        [StringLength(30), Display(Name="Cellphone Number")]
        public string CellphoneNum { get; set; }
        [StringLength(30), Display(Name = "Telephone Number")]
        public string TelephoneNum { get; set; }
        public bool IsRealtyAgent { get; set; }
        public string AboutMessage { get; set; }
        public virtual ICollection<RealtyAd> RealtyAds { get; set; }
        
    }

    //Originally public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //http://stackoverflow.com/questions/19628144/how-can-one-put-application-users-in-the-same-context-as-the-rest-of-the-objects
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<ApplicationUser> IdentityUsers { get; set; }
        public DbSet<RealtyAd> RealtyAds { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<RealtyAdMessage> RealtyAdMessages { get; set; }
        public DbSet<RealtyAdPageView> RealtyAdViews { get; set; }
        public DbSet<RealtyAdImage> RealtyAdImages { get; set; }

        public System.Data.Entity.DbSet<RealEstateApp.Models.RealtyAdCreateViewModel> RealtyAdCreateViewModels { get; set; }

        



    }
}