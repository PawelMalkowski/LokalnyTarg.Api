using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.IServices.Request
{
    public class EditUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
    }
}
