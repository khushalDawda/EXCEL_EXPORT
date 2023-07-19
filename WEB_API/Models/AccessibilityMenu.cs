using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class UserAccessibilityMenu
    {

        [Key]
        public int Id { get; set; }
        public int Accessbility_Id { get; set; }
        public int User_Id { get; set;}
        public int Society_Id { get; set;}
        public bool Add { get; set;}
        public bool Update { get; set;}
        public bool Delete { get; set;}

        public string Extra_1 { get; set; }
        public string Extra_2 { get; set; }
        public string Extra_3 { get; set; }
        public string Extra_4 { get; set; }
        public string Extra_5 { get; set; }
    }
}
