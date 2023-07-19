using API_WEB.DataBaseModels;
using System.Threading.Tasks;

namespace WEB_API.Repository.Interface
{
    public interface IDeposit : IRepository<Deposit>
    {
        Task<Deposit> UpdateAsync(Deposit deposit);
    }
}
