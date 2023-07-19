using API_WEB.DataBaseModels;
using System.Threading.Tasks;

namespace WEB_API.Repository.Interface
{
    public interface IAccount:IRepository<Account>
    {
        Task<Account> UpdateAsync(Account account);
    }
}
