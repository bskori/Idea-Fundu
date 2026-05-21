using Idea_Fundu.Models;

namespace Idea_Fundu.ViewModels
{
    public class DashboardVM
    {
        public int TotalIdeas { get; set; }
        public int TotalInvestments { get; set; }
        public decimal TotalAmountInvested { get; set; }
        public IEnumerable<Idea> RecentIdeas { get; set; }

        public IEnumerable<Investment> RecentInvestments { get; set; }
    }
}
