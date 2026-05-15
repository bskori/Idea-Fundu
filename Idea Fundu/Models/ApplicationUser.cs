using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Idea_Fundu.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public string ProfileImage { get; set; }

        public ICollection<Idea>? Ideas { get; set; }

        public ICollection<Investment>? Investments { get; set; }
    }
}
