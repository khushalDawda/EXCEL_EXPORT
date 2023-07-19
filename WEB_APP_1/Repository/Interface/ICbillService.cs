using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Models;


namespace WEB_APP.Repository.Interface
{
    public interface ICbillService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(List<CbillModel> dto, string token);
        Task<T> UpdateAsync<T>(CbillModel dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);

    }
}
