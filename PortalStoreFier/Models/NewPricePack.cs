using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace PortalStoreFier.Models
{
    public class NewPricePack
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ContractStartingDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ContractEndDate { get; set; }

        // Navigation property
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int PricePackId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        [ForeignKey("PricePackId")]
        public PricePack? PricePack { get; set; }


    }

    




}
