using System.ComponentModel.DataAnnotations;

namespace Idea_Fundu.ViewModels
{
    public class IdeaCreateVM
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Required Fund")]
        public decimal RequiredFund { get; set; }

        [Required]
        public string Category { get; set; }

        public IFormFile? ImageFile { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        public string RiskLevel { get; set; }
        public string Restrictions { get; set; }
    }
}
