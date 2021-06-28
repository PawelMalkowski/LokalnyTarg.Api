using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Data.Sql.DAO
{
    public class Annoucement
    {
        public uint AnnoucementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public uint SuppilerId { get; set; }
        public uint ProductId { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Product Product { get; set; }
    }
}
