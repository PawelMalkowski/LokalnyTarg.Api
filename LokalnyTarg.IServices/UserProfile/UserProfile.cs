using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.IServices.UserProfile
{
    public class UserProfile
    {
        public uint UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }

        public UserAddress Address { get; set; }
    }
}
