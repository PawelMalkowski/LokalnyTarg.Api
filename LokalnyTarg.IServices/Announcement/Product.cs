using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.IServices.Announcement
{
    public class Product
    {
        public uint ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public uint CategoryId { get; set; }
        public string Photo { get; set; }
    }
}
