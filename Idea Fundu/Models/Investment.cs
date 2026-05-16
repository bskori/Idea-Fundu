using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idea_Fundu.Models
{
    public class Investment
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime InvestmentDate { get; set; }

        public string? Suggestions { get; set; }

        public int IdeaId { get; set; }
        
        [ForeignKey("IdeaId")]
        public Idea Idea { get; set; }

        public string InvestorId { get; set; }

        [ForeignKey("InvestorId")]
        public ApplicationUser Investor { get; set; }
    }
}
