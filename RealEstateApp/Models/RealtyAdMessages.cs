using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
    public class RealtyAdMessage
    {
        public long Id { get; set; }
        public virtual long RealtyAdId { get; set; }
        public DateTime DatePosted { get; set; }
        public string PosterId { get; set; }
        public string Email { get; set; }
        public string CellphoneNum { get; set; }
        public string Message { get; set; }
        public virtual RealtyAd RealtyAd { get; set; }
    }
}