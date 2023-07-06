using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PortalStoreFier.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int Employee_id { get; set; }
        [Required]
        public string? FULL_Name { get; set; }
        [Required]
        public string? Id_number { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string? EducationSpecialty { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Position { get; set; }
        [Required]
        public float? Salary { get; set; }
    }



    public enum Gender
    {
        [Display(Name = "Male")]
        Male,

        [Display(Name = "Female")]
        Female
    }

}
