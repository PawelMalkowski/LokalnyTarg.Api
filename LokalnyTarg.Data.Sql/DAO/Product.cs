using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Data.Sql.DAO
{
    public class Product
    {
        public Product()
        {
            Annoucements = new List<Annoucement>();
        }
        public uint ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public float Price { get; set; }
        public uint CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Annoucement> Annoucements {get; set;}
    }
}
