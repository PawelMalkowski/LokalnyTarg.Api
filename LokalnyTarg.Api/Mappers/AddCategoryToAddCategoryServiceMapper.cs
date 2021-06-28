using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokalnyTarg.Api.BindingModels;

namespace LokalnyTarg.Api.Mappers
{
    public class AddCategoryToAddCategoryServiceMapper
    {
        public static IServices.Request.AddCategory AddCategoryToAddCategoryService(AddCategory addCategory)
        {
            var addCategoryService = new IServices.Request.AddCategory
            {
                Name = addCategory.Name
            };
            return addCategoryService;
        }
    }
}
