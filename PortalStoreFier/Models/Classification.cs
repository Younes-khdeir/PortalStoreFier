using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace PortalStoreFier.Models
{
    public class Classification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? ClassificationName { get; set; }

        public List<Customer>? Customers { get; set; }

    }
}
