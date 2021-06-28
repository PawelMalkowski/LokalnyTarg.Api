using LokalnyTarg.IData.Ratio;
using LokalnyTarg.IServices.Ratio;
using LokalnyTarg.IServices.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LokalnyTarg.Services.Ratio
{
    public class RatioService : IRateService
    {

        private readonly IRatioRepository _ratioRepository;

        public RatioService(IRatioRepository ratioRepository)
        {
            _ratioRepository = ratioRepository;
        }

        public async Task AddRatio(string userId, AddRate addRate)
        {
            if (! await _ratioRepository.CheckUserProfileIsNotEmpty(userId)) throw new Exception("Profile cannot be empty");
            if(! await _ratioRepository.SupplierExist((int)addRate.SuplierId)) throw new Exception("Supplier not exist");
            var rate = new Domain.Ratio.AddRatio(addRate.SuplierId, addRate.Value, addRate.Description);
            await _ratioRepository.AddRatio(userId, rate);
        }
    }
}
