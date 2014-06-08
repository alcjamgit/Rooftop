using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
  public class RealtyAdImageDefault
  {
    public int Id { get; set; }

    [ForeignKey("RealtyAd")]
    public virtual int RealtyAd_Id { get; set; }
    
    [ForeignKey("RealtyAdImage")]
    public virtual long RealtyAdImage_Id { get; set; }

    //denormalized for quick retrieval
    public string FileName { get; set; }
    public virtual RealtyAd RealtyAd { get; set; }
    public virtual RealtyAdImage RealtyAdImage { get; set; }
  }
}