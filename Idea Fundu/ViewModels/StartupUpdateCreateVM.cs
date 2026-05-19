using System.ComponentModel.DataAnnotations;

namespace Idea_Fundu.ViewModels
{
    public class StartupUpdateCreateVM
    {
        public int IdeaId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
