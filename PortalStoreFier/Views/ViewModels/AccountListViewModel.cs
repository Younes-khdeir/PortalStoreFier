using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortalStoreFier.ViewModels
{
    public class AccountListViewModel
    {
        [Display(Name = "Account Id", Description = "Account Id")]
        public int Id { get; set; }

        [Display(Name = "Account Name", Description = "Acccount name")]
        [Required]
        public string AccountName { get; set; }


    }
}
