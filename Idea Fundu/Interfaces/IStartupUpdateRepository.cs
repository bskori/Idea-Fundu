using Idea_Fundu.Models;

namespace Idea_Fundu.Interfaces
{
    public interface IStartupUpdateRepository
    {
        Task AddUpdateAsync(StartupUpdate update);

        Task<IEnumerable<StartupUpdate>> GetUpdatesByIdAsync(int ideaId);
    }
}
