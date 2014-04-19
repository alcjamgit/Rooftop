using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
    public class City
    {
        public int Id { get; set; }
        [StringLength(100), Display(Name="City Name")]
        public string Name { get; set; }
        public short Region { get; set; }


    }
}