using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateApp.Models
{
    public class AppUser : IUser
    {
        public virtual string Id { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        public virtual string UserName { get; set; }
    }

}