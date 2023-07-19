using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Models;

namespace WEB_API.Repository.Interface
{
    public interface ILoan:IRepository<Loan>
    {
        Task<Loan> UpdateAsync(Loan loan);
    }
}
