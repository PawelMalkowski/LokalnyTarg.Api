using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.IData.UserProfile
{
    public class UserAddress
    {
        public string City { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Postcode { get; private set; }
        public UserAddress(string city, string street, string number, string postcode)
        {
            City = city;
            Street = street;
            Number = number;
            Postcode = postcode;
        }
    }
}
