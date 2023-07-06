using PortalStoreFier.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortalStoreFier.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        
        [Required]
        public int Employee_id { get; set; }
        
        [Required]
        public string? FULL_Name { get; set; }

        [Required]
        public string? Id_number { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        [Required]
        public string? EducationSpecialty { get; set; }

        [Required]
        public string? Phone { get; set; }
        
        [Required]
        public string? Position { get; set; }

        [Required]
        public float? Salary { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string? ConfirmPassword { get; set; }

    }
}
