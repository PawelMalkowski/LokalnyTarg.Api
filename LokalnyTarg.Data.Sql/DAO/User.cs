using System.Collections.Generic;

namespace LokalnyTarg.Data.Sql.DAO
{
    public class User
    {
        public User()
        {
            FavoriteSuppilliers = new List<FavoriteSuppillier>(); // in <> outputs psad
        }
        public uint UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public uint AddressId { get; set; }
        public string EntityId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<FavoriteSuppillier> FavoriteSuppilliers { get; set; }
    }
} 
