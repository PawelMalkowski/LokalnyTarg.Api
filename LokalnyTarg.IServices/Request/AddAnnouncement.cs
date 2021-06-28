using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.IServices.Request
{
    public class AddAnnouncement
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string Photo { get; set; }
        public string ProductName { get; set; }
        public string ProductDescriptions { get; set; }
        public double Price { get; set; }
        public uint CategoryId { get; set; }
    }
}
