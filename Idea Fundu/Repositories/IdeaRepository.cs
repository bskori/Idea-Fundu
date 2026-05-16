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
    }
}
