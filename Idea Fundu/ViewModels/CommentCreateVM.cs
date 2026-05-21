using System.ComponentModel.DataAnnotations;

namespace Idea_Fundu.ViewModels
{
    public class CommentCreateVM
    {
        public int IdeaId { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }
    }
}
