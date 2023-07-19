using API_WEB.Data;
using API_WEB.DataBaseModels;
using System.Threading.Tasks;
using WEB_API.Repository.Interface;

namespace WEB_API.Repository.ServiceClass
{
    public class AccountDBService : Repository<Account>, IAccount
    {
        private readonly ApplicationDbContext _db;
        public AccountDBService(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Account> UpdateAsync(Account entity)
        {
            _db.Accounts.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
