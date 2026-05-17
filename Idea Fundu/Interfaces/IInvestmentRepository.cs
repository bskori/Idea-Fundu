using Idea_Fundu.Models;

namespace Idea_Fundu.Interfaces
{
    public interface IInvestmentRepository
    {
        Task AddInvestmentAsync(Investment investment);

        Task<IEnumerable<Investment>> GetInvestmentsByIdeaAsync(int ideaId);

        Task<IEnumerable<Investment>> GetInvestmentByUserAsync(string userId);
    }
}
