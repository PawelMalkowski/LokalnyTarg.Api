using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.IData.UserProfile
{
    public class UserProfile
    {
        public uint UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Description { get; private set; }

        public UserAddress Address { get; private set; }

        public UserProfile(string firstName, string lastName, string description,
            string city, string street, string number, string postcode, uint userId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Description = description;
            Address = new UserAddress(city, street, number, postcode);
        }
    }
}
