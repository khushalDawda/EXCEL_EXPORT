using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Models;

namespace WEB_API.Repository.Interface
{
    interface IMenuMaster
    {
        IEnumerable<MenuMaster> GetMenuMaster();
        IEnumerable<MenuMaster> GetMenuMaster(String UserRole);
    }
}
