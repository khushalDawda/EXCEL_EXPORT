using API_WEB.Data;
using API_WEB.DataBaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB_API.Repository.Interface;

namespace WEB_API.Repository.ServiceClass
{
    public class CbillDBService : Repository<Cbill>, ICbill
    {
        private readonly ApplicationDbContext _db;
        public CbillDBService(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Cbill> UpdateAsync(Cbill entity)
        {
            _db.Cbills.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteDataAsync(List<Cbill> entities)
        {
            _db.Cbills.RemoveRange(entities);
            return true;
        }


    }
}
