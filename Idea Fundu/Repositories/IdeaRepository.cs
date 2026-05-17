using Idea_Fundu.Data;
using Idea_Fundu.Interfaces;
using Idea_Fundu.Models;
using Microsoft.EntityFrameworkCore;

namespace Idea_Fundu.Repositories
{
    public class IdeaRepository : IIdeaRepository
    {
        private readonly ApplicationDbContext _context;

        public IdeaRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddIdeaAsync(Idea idea)
        {
            await _context.Ideas.AddAsync(idea);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIdeaAsync(int id)
        {
            var idea = await _context.Ideas.FindAsync(id);
            if (idea != null) 
            {
                _context.Ideas.Remove(idea);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Idea>> GetAllIdeasAsync()
        {
            return await _context.Ideas.ToListAsync();
        }

        public async Task<Idea> GetIdeaDetailsByIdAsync(int id)
        {
            return await _context.Ideas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateIdeaAsync(Idea idea)
        {
            _context.Ideas.Update(idea);
            await _context.SaveChangesAsync();
        }

        public async Task<Idea> GetIdeaByIdAsync(int id)
        {
            return await _context.Ideas.FindAsync(id);
        }

        public async Task<IEnumerable<Idea>> SearchIdeasAsync(string SearchTerm, string category)
        {
            var query =  _context.Ideas.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(x => x.Title.Contains(SearchTerm) || x.Description.Contains(SearchTerm));
            }

            if(!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Category == category);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Idea>> GetIdeasByUserAsync(string userId)
        {
            return await _context.Ideas.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<decimal> GetTotalInvestmentAsync(int ideaId)
        {
            return await _context.Investments.Where(x => x.IdeaId == ideaId).SumAsync(x => (decimal?)x.Amount) ?? 0;
        }
    }
}
