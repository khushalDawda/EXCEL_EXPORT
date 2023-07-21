using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Models
{
    public class UserWiseMenu
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string MenuID { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public string USE_YN { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
