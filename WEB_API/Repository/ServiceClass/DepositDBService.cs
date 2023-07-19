using System.Threading.Tasks;
using WEB_API.Data;
using WEB_API.Models;
using WEB_API.Repository.Interface;

namespace WEB_API.Repository.ServiceClass
{
    public class DepositDBService : Repository<Deposit>, IDeposit
    {
        private readonly ApplicationDbContext _db;
        public DepositDBService(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Deposit> UpdateAsync(Deposit entity)
        {
            _db.Deposits.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
