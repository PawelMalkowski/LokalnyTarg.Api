using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.IData.Category;
using LokalnyTarg.IServices.Category;
using LokalnyTarg.IServices.Request;
using LokalnyTarg.IServices.User;

namespace LokalnyTarg.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategory(AddCategory addCategory)
        {
            if (string.IsNullOrWhiteSpace(addCategory.Name)) throw new Exception("Name can't be empty");
            var domainCategory = new Domain.Category.AddCategory(addCategory.Name);
            await _categoryRepository.AddCategory(domainCategory);
        }

        public async Task<List<IServices.Category.Category>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            return categories.Select(x => new IServices.Category.Category
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
