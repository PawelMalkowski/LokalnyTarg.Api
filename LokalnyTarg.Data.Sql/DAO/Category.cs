using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Data.Sql.DAO
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public uint CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
