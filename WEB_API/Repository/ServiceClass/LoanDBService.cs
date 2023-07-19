using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Data;
using WEB_API.Models;
using WEB_API.Repository.Interface;

namespace WEB_API.Repository.ServiceClass
{
    public class LoanDBService : Repository<Loan>, ILoan
    {
        private readonly ApplicationDbContext _db;
        public LoanDBService(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Loan> UpdateAsync(Loan entity)
        {
            _db.Loans.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
