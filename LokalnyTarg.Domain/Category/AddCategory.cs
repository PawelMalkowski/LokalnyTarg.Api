using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Domain.Category
{
    public class AddCategory
    {
        public string Name { get; private set; }

        public AddCategory(string name)
        {
            Name = name;
        }
    }
    
}
