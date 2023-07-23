using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Models
{
    public class UserWiseMenuModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string MenuID { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public string USE_YN { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<UserModel> userModelsList { get; set; }
        public List<MenuMasterModel> MenuMasterList { get; set; }
    }
}
