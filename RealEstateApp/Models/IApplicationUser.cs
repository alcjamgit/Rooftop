using System;
namespace RealEstateApp.Models
{
  interface IApplicationUser
  {
    string AboutMessage { get; set; }
    string CellphoneNum { get; set; }
    DateTime DateJoined { get; set; }
    string Email { get; set; }
    string FirstName { get; set; }
    bool IsRealtyAgent { get; set; }
    string LastName { get; set; }
    string ProfilePhotoFileName { get; set; }
    System.Collections.Generic.ICollection<RealtyAd> RealtyAds { get; set; }
    string TelephoneNum { get; set; }
  }
}
