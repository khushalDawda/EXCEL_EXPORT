using API_WEB.DataBaseModels;
using System.Threading.Tasks;
using ViewModels.Models;

namespace WEB_API.Repository.Interface
{

    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseModel> Login(LoginRequestModel loginRequestDTO);
        Task<UserModel> Register(RegisterationRequestModel registerationRequestDTO);
        ApplicationUser GetUserInfo(string username);

    }

}
