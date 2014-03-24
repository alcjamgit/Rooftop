using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
    public class RealtyDataContext: DbContext

    {
        //Use same connection string as the db context
        public RealtyDataContext()
            :base("DefaultConnection")
        {
        }

        public DbSet<RealtyAd> RealtyAds { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<RealtyAdMessage> RealtyAdMessages { get; set; }
        public DbSet<RealtyAdView> RealtyAdViews { get; set; }
        public DbSet<RealtyAdImage> RealtyAdImages { get; set; }

    }
}