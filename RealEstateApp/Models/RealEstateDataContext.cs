using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
    public class RealEstateDataContext: DbContext

    {
        //Use same connection string as the db context
        public RealEstateDataContext()
            :base("DefaultConnection")
        {
        }

        public DbSet<RealtyAd> RealtyAds { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}