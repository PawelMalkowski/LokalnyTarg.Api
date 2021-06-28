using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.Domain.Ratio
{
    public class AddRatio
    {
        public uint SuplierId { get; private set; }
        public uint Value { get; private set; }
        public string Description { get; private set; }

        public AddRatio(uint suplierId, uint value, string description)
        {
            SuplierId = suplierId;
            Value = value;
            Description = description;
        }
    }
}
