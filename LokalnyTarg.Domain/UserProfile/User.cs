using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Domain.UserProfile
{
    public class User
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Description { get; private set; }
        public Address Address { get; private set; }

        public User(string firstName, string lastName, string description,
            string city, string street, string number, string postcode)
        {
            FirstName = firstName;
            LastName = lastName;
            Description = description;
            Address = new Address(city, street, number, postcode);
        }
    }
}
