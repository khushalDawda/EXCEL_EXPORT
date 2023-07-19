using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Models;

namespace WEB_APP.Repository.Interface
{
    public interface ILoanservice
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(List<LoanModel> dto, string token);
        Task<T> UpdateAsync<T>(LoanModel dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);

    }
}
