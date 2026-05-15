using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Idea_Fundu.Models
{
    public class Agreement
    {
        public int Id { get; set; }

        [Required]
        public string AgreementFile { get; set; }

        public DateTime AgreementDate { get; set; }=DateTime.Now;

        public int InvestmentId { get; set; }

        [ForeignKey("InvestmentId")]
        public Investment Investment { get; set; }
    }
}
