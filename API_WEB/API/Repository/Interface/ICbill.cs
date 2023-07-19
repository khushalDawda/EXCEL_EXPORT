using API_WEB.DataBaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB_API.Repository.Interface
{
    public interface ICbill : IRepository<Cbill>
    {
        Task<Cbill> UpdateAsync(Cbill account);
        Task<bool> DeleteDataAsync(List<Cbill> entities);
    }
}
