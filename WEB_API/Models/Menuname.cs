using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class AccessibilityMenu
    {
        [Key]
        public int Accessbility_Id { get; set; }
        public string Menu_Name { get; set; }
        public string Status { get; set; }


    }
}
