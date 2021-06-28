using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LokalnyTarg.Api.BindingModels
{
    public class Address
    {
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
        [MaxLength(10)]
        public string Number { get; set; }
        [MinLength(6)]
        [MaxLength(6)]
        public string Postcode { get; set; }
    }
}
