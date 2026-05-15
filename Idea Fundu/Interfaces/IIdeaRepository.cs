using Idea_Fundu.Models;

namespace Idea_Fundu.Interfaces
{
    public interface IIdeaRepository
    {
        Task<IEnumerable<Idea>> GetAllIdeasAsync();
        
        Task<Idea> GetIdeaByIdAsync(int id);

        Task AddIdeaAsync(Idea idea);

        Task UpdateIdeaAsync(Idea idea);

        Task DeleteIdeaAsync(int id);

    }
}
