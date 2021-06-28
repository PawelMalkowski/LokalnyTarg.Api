using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Domain.Announcement
{
    public class AddAnnouncement
    {
        public string Title { get; private set; }

        public string Description { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescriptions { get;private set; }
        public double Price { get;private set; }
        public uint CategoryId { get;private set; }
        public string Photo { get; private set; }
        public AddAnnouncement(string title, string description, string productName,
            string productDescription, double price, uint categoryId, string photo)
        {
            Title = title;
            Description = description;
            ProductName = productName;
            ProductDescriptions = productDescription;
            Price = price;
            CategoryId = categoryId;
            Photo = photo;
        }
    }
}
