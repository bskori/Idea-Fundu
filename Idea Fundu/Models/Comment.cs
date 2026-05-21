using System.ComponentModel.DataAnnotations;

namespace Idea_Fundu.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public int IdeaId { get; set; }
        public Idea Idea { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
