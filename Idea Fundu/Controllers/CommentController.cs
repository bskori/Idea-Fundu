using Idea_Fundu.Interfaces;
using Idea_Fundu.Models;
using Idea_Fundu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Idea_Fundu.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repository;
        public CommentController(ICommentRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateVM vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                Comment comment = new Comment
                {
                    IdeaId = vm.IdeaId,
                    Message = vm.Message,
                    CreatedDate = DateTime.Now,
                    UserId = userId
                };

                await _repository.AddCommentAsync(comment);
            }

            return RedirectToAction("Details", "Idea", new { id = vm.IdeaId });
        }
    }
}
