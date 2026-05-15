using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idea_Fundu.Models
{
    public class Idea
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName="decimal(18,2)")]
        public decimal RequiredFund { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        public string? RiskLevel { get; set; }
        public string? Restrictions { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public ICollection<Document>? Documents { get; set; }

        public ICollection<Investment>? Investments { get; set; }

        public ICollection<ProgressUpdate>? ProgressUpdates { get; set; }
    }
}
