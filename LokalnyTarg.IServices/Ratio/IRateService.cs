using LokalnyTarg.IServices.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LokalnyTarg.IServices.Ratio
{
    public interface IRateService
    {
        public Task AddRatio(string userId, AddRate addRate);
       // public Task GetRatio(uint userId);
    }
}
