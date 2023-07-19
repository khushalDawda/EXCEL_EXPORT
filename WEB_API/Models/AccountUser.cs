using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Soc_Id { get; set; }
        public string Soc_Name { get; set; }

    }
}
