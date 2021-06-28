using LokalnyTarg.Domain.Ratio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LokalnyTarg.IData.Ratio
{
    public interface IRatioRepository
    {
        public Task AddRatio(string userId, AddRatio addRatio);
        public Task<bool> CheckUserProfileIsNotEmpty(string userId);
        public Task<bool> SupplierExist(int SupplierId);
    }
}
