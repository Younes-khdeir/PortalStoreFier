using System.ComponentModel.DataAnnotations;

namespace PortalStoreFier.ViewModels
{
    public class AccountCreateViewModel
    {
        [Display(Name = "Account Id", Description = "Account Id")]
        public int Id { get; set; }

        [Display(Name = "Account Name", Description = "Acccount name")]
        [Required]
        public string AccountName { get; set; }

    }
}
