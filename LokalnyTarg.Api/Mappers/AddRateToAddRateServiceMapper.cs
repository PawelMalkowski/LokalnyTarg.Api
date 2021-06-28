using LokalnyTarg.Api.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LokalnyTarg.Api.Mappers
{
    public class AddRateToAddRateServiceMapper
    {
        public static IServices.Request.AddRate AddRateToAddRateService(AddRate addRate)
        {
            return new IServices.Request.AddRate
            {
                Value= addRate.Value,
                SuplierId= addRate.SuplierId,
                Description= addRate.Description
            };
        }
    }
}
