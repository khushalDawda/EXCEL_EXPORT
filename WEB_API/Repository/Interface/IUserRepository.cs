using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Models;
using WEB_API.Models;

namespace WEB_API.Repository.Interface
{

    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseModel> Login(LoginRequestModel loginRequestDTO);
        Task<UserModel> Register(RegisterationRequestModel registerationRequestDTO);
        ApplicationUser GetUserInfo(string username);
        bool AnyUserPresent();
        List<string> GetRoles();
        string GetMaxSocietyId();

    }

}
