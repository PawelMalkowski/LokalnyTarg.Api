using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Data.Sql.DAO
{
    public class Rate
    {
        public uint RateId { get; set; }
        public uint UserId { get; set; }
        public uint SupplierId { get; set; }
        public int RateValue { get; set; }
        public string Description { get; set; }

        public virtual Supplier Supplier { get; set; } // green type, whit name 
    }
}
