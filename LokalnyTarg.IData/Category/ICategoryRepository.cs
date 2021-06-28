using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.Domain.Category;

namespace LokalnyTarg.IData.Category
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task AddCategory(AddCategory addCategory);
    }
}
