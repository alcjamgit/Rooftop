using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateApp.ViewModels
{
  public class AgentProfile
  {
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Display(Name="Name")]
    public string FullName { get { return FirstName + " " + LastName; } }
    public string UserName { get; set; }
    public string Email { get; set; }

    [Display(Name="Member Since"), DisplayFormat(DataFormatString = "{0:M/d/yyyy}")]
    public DateTime DateJoined { get; set; }
    public string ProfilePhotoFileName { get; set; }
    public string ProfilePhotoUrl { get { return string.Format("~/{0}/{1}/{2}", Helpers.Config.Directories.Images, ProfilePhotoFileName); } }
    

  }
}