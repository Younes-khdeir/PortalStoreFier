using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortalStoreFier.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required]
        public string?  Email { get; set; }
    }
}
