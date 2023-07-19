using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Models;

namespace WEB_APP.Repository.Interface
{
    public interface IAccountService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(List<AccountModel> dto, string token);
        Task<T> UpdateAsync<T>(AccountModel dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);

    }
}
