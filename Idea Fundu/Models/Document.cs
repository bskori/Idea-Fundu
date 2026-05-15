using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idea_Fundu.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public DateTime UploadedDate { get; set; } = DateTime.Now;

        public int IdeaId { get; set; }

        [ForeignKey("IdeaId")]
        public Idea Idea { get; set; }
    }
}
