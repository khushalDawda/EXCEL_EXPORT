using System.Threading.Tasks;
using WEB_API.Models;

namespace WEB_API.Repository.Interface
{
    public interface IDeposit : IRepository<Deposit>
    {
        Task<Deposit> UpdateAsync(Deposit deposit);
    }
}
