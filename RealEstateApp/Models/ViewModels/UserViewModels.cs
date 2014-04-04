using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
  public class UpdateUserProfileViewModel
  {
    public string Id { get; private set; }
    [StringLength(50), Display(Name = "First Name")]
    public string FirstName { get; set; }
    [StringLength(50), Display(Name = "Last Name")]
    public string LastName { get; set; }
    [StringLength(30), Display(Name = "Cellphone Number")]
    public string CellphoneNum { get; set; }
    [StringLength(30), Display(Name = "Telephone Number")]
    public string TelephoneNum { get; set; }
    public bool IsRealtyAgent { get; set; }

  }
}