using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalStoreFier.Models
{
    public class PricePack
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Service { get; set; }

        [MaxLength(5000)]
        [Required]
        public string? Details { get; set; }


        [MaxLength(5000)]
        [Required]
        public string? Notes { get; set; }

        [Required]
        public string? Cost { get; set; }

        //[Required]
        //public int NewPricePackId { get; set; } // Required foreign key property

        //[ForeignKey("NewPricePackId")]
        //public NewPricePack? NewPricePack { get; set; } // Required reference navigation to principal

        public List<NewPricePack>? NewPricePack { get; set; }


    }
}
