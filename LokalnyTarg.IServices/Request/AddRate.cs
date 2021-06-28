using System;
using System.Collections.Generic;
using System.Text;

namespace LokalnyTarg.IServices.Request
{
    public class AddRate
    {
        public uint SuplierId { get; set; }
        public uint Value { get; set; }
        public string Description { get; set; }
    }
}
