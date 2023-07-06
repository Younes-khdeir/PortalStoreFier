using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace PortalStoreFier.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? ContactNumber { get; set; }
        //[Required]
        //public string? Classification { get; set; }
        [Required]
        public WorkplaceGovernorate? WorkplaceGovernorate { get; set; }
        [Required]
        public WorkplaceCity? WorkplaceCity { get; set; }
        [Required]
        public string? WorkplaceResidentialArea { get; set; }
        [Required]
        public string? Facebook_page { get; set; }
        [Required]
        public string? Instagram_page { get; set; }
        [Required]
        public string? Whatsapp_business_account { get; set; }
        [Required]
        public string? Start_WorkingHours { get; set; }
        [Required]
        public string? End_WorkingHours { get; set; }
        [Required]
        public string? Employee_Name { get; set; }

        [Required]
        public string? OfficialEmail { get; set; }
        public string? TikTok_page { get; set; }
        public string? Twitter_page { get; set; }
        public string? Linkedin_page { get; set; }
        public string? YouTube_page { get; set; }
        public string? GeographicalLocation_GPS { get; set; }
        public string? CompanyLogo { get; set; }
        public string? CompanyWebsite { get; set; }



        // Navigation property
        public List<CustomerActivity>? CustomerActivity { get; set; } 
        public List<RelationManagement>? RelationManagement { get; set; }

        [Required]
        public int ClassificationId { get; set; } // Required foreign key property

        [ForeignKey("ClassificationId")]
        public Classification? Classification { get; set; } // Required reference navigation to principal


        public List<NewPricePack>? NewPricePack { get; set; }

        //[Required]
        //public int NewPricePackId { get; set; } // Required foreign key property

        //[ForeignKey("NewPricePackId")]
        //public NewPricePack? NewPricePack { get; set; } // Required reference navigation to principal

    }


    public enum WorkplaceGovernorate
    {
        [Display(Name = "الضفة الغربية ")]
        WestBank,

        [Display(Name = "قطاع غزة  ")]
        Gaza
    }
    public enum WorkplaceCity
    {
        [Display(Name = "القدس")]
        Jerusalem,

        [Display(Name = "نابلس ")]
        Nablus,

        [Display(Name = "أريحا ")]
        Jericho,

        [Display(Name = "جنين ")]
        Embryo,

        [Display(Name = "طول كرم ")]
        KaramLength,

        [Display(Name = "الخليل ")]
        Hebron,

        [Display(Name = "قلقيلية ")]
        Qalqilya,


        [Display(Name = "طوباس ")]
        Tubas,

        [Display(Name = "سلفيت ")]
        Salfit,

        [Display(Name = "   رام الله والبيرة ")]
        RamallahAlBireh,

        [Display(Name = "بيت لحم ")]
        RBethlehem,



    }
}
