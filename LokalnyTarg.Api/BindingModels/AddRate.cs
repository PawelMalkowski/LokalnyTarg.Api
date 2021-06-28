using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LokalnyTarg.Api.BindingModels
{
    public class AddRate
    {
        [Required]
        public uint SuplierId { get; set; }
        [Required]
        [Range(1,10)]
        public uint Value { get; set; }
        public string Description { get; set; }
    }
}
