using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RealEstateApp.Models
{
    public class RealtyAdImage
    {
        public long Id { get; set; }
        public long RealtyAdId { get; set; }
        [Url]
        public string Url { get; set; }

    }
}
