using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.IServices.Category;
using LokalnyTarg.IServices.Request;

namespace LokalnyTarg.IServices.Category
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetCategories();
        public Task AddCategory(AddCategory addCategory);
    }

}
