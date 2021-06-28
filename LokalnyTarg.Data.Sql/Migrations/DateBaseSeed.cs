using System;
using System.Collections.Generic;
using System.Text;
using LokalnyTarg.Data.Sql.DAO;

namespace LokalnyTarg.Data.Sql.Migrations
{
    public class DateBaseSeed
    {
        private readonly LokalnyTargDBContext _context;

        public DateBaseSeed(LokalnyTargDBContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            var categoryList = BuilCategoryList();
            _context.Category.AddRange(categoryList);
            _context.SaveChanges();
        }

        private IEnumerable<DAO.Category> BuilCategoryList()
        {
            var categoriesList = new List<DAO.Category>();
            string[] categoryNameList = { "warzywa","owoce", "pieczywo i wypieki", "jajka", "produkty mięsne", "produkty mleczne" };
            foreach (var categoryName in categoryNameList)
            {
                var category = new DAO.Category
                {
                    Name = categoryName
                };
                categoriesList.Add(category);
            }

            return categoriesList;
        }

    }
}
