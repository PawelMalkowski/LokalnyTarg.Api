using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Data.Sql.DAO
{
    public class Address
    {
        public Address()
        {
            Suppliers = new List<Supplier>();
        }
        public uint AddressId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Postcode { get; set; }
        
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }

}
