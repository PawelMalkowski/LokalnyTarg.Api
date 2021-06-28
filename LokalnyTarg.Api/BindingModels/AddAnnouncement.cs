using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LokalnyTarg.Api.BindingModels
{
    public class AddAnnouncement
    {
        [Required]
        [MaxLength(70)]
        public string Title { get; set; }

        [Required] 
        public string Description { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string Photo { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public uint CategoryId { get; set; }


    }
}
