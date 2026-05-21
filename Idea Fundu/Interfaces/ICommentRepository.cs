using Idea_Fundu.Models;

namespace Idea_Fundu.Interfaces
{
    public interface ICommentRepository
    {
        Task AddCommentAsync(Comment comment);

        Task<IEnumerable<Comment>> GetCommentsByIdeaAsync(int ideaId);
    }
}
