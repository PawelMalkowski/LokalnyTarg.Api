using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalnyTarg.IData.Category;
using LokalnyTarg.Data.Sql.DAO;
using Microsoft.EntityFrameworkCore;
using LokalnyTarg.Domain.Category;

namespace LokalnyTarg.Data.Sql.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LokalnyTargDBContext _context;

        public CategoryRepository(LokalnyTargDBContext context)
        {
            _context = context;
        }

        public async Task<List<IData.Category.Category>> GetCategories()
        {
            return await _context.Category.Select(x => new IData.Category.Category
            {
                Id = x.CategoryId,
                Name = x.Name
            }).ToListAsync();
        }


        public async Task AddCategory(AddCategory addCategory)
        {
            var categoryDAO = new DAO.Category
            {
                Name = addCategory.Name
            };
            await _context.AddAsync(categoryDAO);
            await _context.SaveChangesAsync();
        }
    }
}
