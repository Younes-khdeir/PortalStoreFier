using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace PortalStoreFier.Models
{
    public class RelationManagement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Employee_Name { get; set; }

        [Required]
        public Result_type Result_type { get; set; }
        
        [Required]
        public Result Result { get; set; }
        public string? Notes { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Required]
        public int CustomerId { get; set; } // Foreign key property

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; } // Navigation property


    }



    public enum Result_type
    {
        [Display(Name = "جاهز وسيتم توقيع العقد")]
        Done,

        [Display(Name = "معلق ")]
        Stuck,

        [Display(Name = "قيد الانتظار ")]
        Working_on_it,

        [Display(Name = "رفض ")]
        reject

    }


    public enum Result
    {
        [Display(Name = "Call")]
        Call,

        [Display(Name = "Visit")]
        Visit,

        [Display(Name = "Call and Visit")]
        Call_and_Visit
    }




}
