using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Models;

namespace WEB_APP.Repository.Interface
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestModel objToCreate);
        Task<T> RegisterAsync<T>(RegisterationRequestModel objToCreate);
        Task<T> IsAnyUserPresent<T>();
        Task<T> GetRoles<T>();
        Task<T> GetUserInfo<T>(string username);
        Task<T> GetMaxSocietyId<T>();
    }
}
