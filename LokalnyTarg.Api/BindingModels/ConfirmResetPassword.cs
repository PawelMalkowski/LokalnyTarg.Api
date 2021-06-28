using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LokalnyTarg.Api.BindingModels
{
    public class ConfirmResetPassword
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
