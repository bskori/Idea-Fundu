using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idea_Fundu.Models
{
    public class ProgressUpdate
    {
        public int Id { get; set; }

        [Required]
        public string UpdateDetails { get; set; }

        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public int CompletionPercentage { get; set; }

        public int IdeaId { get; set; }

        [ForeignKey("IdeaId")]
        public Idea Idea { get; set; }
    }
}
