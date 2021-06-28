using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Data.Sql.DAO
{
    public class Supplier
    {
        public Supplier() 
        {
            FavoriteSuppilliers = new List<FavoriteSuppillier>();
            Annoucements = new List<Annoucement>();
            Rates = new List<Rate>();
        } 


        public uint SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint AddressId { get; set; }
        public uint UsertId { get; set; }

        
        public virtual Address Address { get; set; }
        public virtual ICollection<FavoriteSuppillier> FavoriteSuppilliers { get; set; }
        public virtual ICollection<Annoucement> Annoucements { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
