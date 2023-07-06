using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalStoreFier.Models
{
    public class CustomerActivity
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string? Employee_Name { get; set; }

        [Required]
        public PostType? PostType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePostPublished { get; set; }


        [MaxLength(5000)]
        public string? PostIdea { get; set; }

        [MaxLength(5000)]
        public string? PostContent { get; set; }

        public string? PostDesign{ get; set; }

        public string? FacebookPostLink { get; set; }

        public string? InstagramPostLink { get; set; }

        public string? Notes { get; set; }

        public IsSponsored? IsSponsored { get; set; }

        public float? SponsoredValue { get; set; }



        [Required]
        public int CustomerId { get; set; } // Foreign key property

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; } // Navigation property

    }

    public enum PostType
    {
        [Display(Name = "Design Photos")]
        DesignPhotos,

        [Display(Name = "Video editing")]
        VideoEditing
    }

    public enum IsSponsored
    {
        [Display(Name = "Yes")]
        Yes,

        [Display(Name = "No")]
        No
    }

}
