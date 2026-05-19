using System.ComponentModel.DataAnnotations;

namespace Idea_Fundu.Models
{
    public class StartupUpdate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int IdeaId { get; set; }

        public Idea Idea { get; set; }
    }
}
