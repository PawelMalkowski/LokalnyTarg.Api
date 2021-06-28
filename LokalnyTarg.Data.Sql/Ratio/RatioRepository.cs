using LokalnyTarg.Domain.Ratio;
using LokalnyTarg.IData.Ratio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LokalnyTarg.Data.Sql.Ratio
{
    class RatioRepository : IRatioRepository
    {
        private readonly LokalnyTargDBContext _context;

        public RatioRepository(LokalnyTargDBContext context)
        {
            _context = context;
        }

        public async Task AddRatio(string userId, AddRatio addRatio)
        {
            var userNormalId = await _context.User.Where(x=>x.EntityId==userId).Select(x=>x.UserId).FirstAsync();
            var ratio = new DAO.Rate
            {
                UserId = userNormalId,
                Description= addRatio.Description,
                SupplierId= addRatio.SuplierId,
                RateValue= (int)addRatio.Value
            };
            await _context.AddAsync(ratio);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckUserProfileIsNotEmpty(string userId)
        {
           return await _context.User.AnyAsync(x => x.EntityId == userId);
        }

        public async Task<bool> SupplierExist(int SupplierId)
        {
            return await _context.User.AnyAsync(x => x.UserId == SupplierId);
        }
    }
}
