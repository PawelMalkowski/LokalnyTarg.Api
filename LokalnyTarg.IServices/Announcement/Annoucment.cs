using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.IServices.Announcement
{
    public class Annoucment
    {
        public uint AnnoucementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public uint SuppilerId { get; set; }
        public string SuplierName { get; set; }
    

        public Product Product { get; set; }
    }
}
