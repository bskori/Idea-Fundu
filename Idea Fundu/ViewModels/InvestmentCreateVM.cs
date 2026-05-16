using System.ComponentModel.DataAnnotations;

namespace Idea_Fundu.ViewModels
{
    public class InvestmentCreateVM
    {
        public int IdeaId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string? Suggestions { get; set; }
    }
}
