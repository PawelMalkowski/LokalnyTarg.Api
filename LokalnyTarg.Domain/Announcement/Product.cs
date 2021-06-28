using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Domain.Announcement
{
    public class Product
    {
        public uint ProductId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public float Price { get; private set; }
        public uint CategoryId { get; private set; }
        public string Photo { get; private set; }

        public Product(uint productId, string name, string description, float price, uint categoryId,string photo)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
            Photo = photo;
        }
    }
}
