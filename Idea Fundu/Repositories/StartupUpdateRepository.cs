using Idea_Fundu.Data;
using Idea_Fundu.Interfaces;
using Idea_Fundu.Models;
using Microsoft.EntityFrameworkCore;

namespace Idea_Fundu.Repositories
{
    public class StartupUpdateRepository : IStartupUpdateRepository
    {
        private readonly ApplicationDbContext _context;

        public StartupUpdateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUpdateAsync(StartupUpdate update)
        {
            await _context.StartupUpdates.AddAsync(update);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StartupUpdate>> GetUpdatesByIdAsync(int ideaId)
        {
            return await _context.StartupUpdates.Where(x => x.IdeaId == ideaId).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }
    }
}
