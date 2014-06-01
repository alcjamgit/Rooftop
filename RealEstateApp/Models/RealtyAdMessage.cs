using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
    public class RealtyAdMessage
    {

        public long Id { get; set; }
        [ForeignKey("RealtyAd")]
        public virtual int RealtyAd_Id { get; set; }

        [DataType(DataType.Date), Display(Name = "Date Posted"), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePosted { get; set; }
        public string PosterId { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string CellphoneNum { get; set; }
        public string Message { get; set; }
        public virtual RealtyAd RealtyAd { get; set; }
    }
}