using Idea_Fundu.Data;
using Idea_Fundu.Interfaces;
using Idea_Fundu.Models;
using Microsoft.EntityFrameworkCore;

namespace Idea_Fundu.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly ApplicationDbContext _context;

        public InvestmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddInvestmentAsync(Investment investment)
        {
            await _context.Investments.AddAsync(investment);
        }

        public async Task<IEnumerable<Investment>> GetInvestmentsByIdeaAsync(int ideaId)
        {
            return await _context.Investments.Where(x => x.IdeaId == ideaId).ToListAsync();
        }
    }
}
