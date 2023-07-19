using API_WEB.DataBaseModels;
using System.Threading.Tasks;

namespace WEB_API.Repository.Interface
{
    public interface ILoan:IRepository<Loan>
    {
        Task<Loan> UpdateAsync(Loan loan);
    }
}
