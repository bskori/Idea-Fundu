using Idea_Fundu.Interfaces;
using Idea_Fundu.Models;
using Idea_Fundu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Idea_Fundu.Controllers
{
    public class StartupUpdateController : Controller
    {
        private readonly IStartupUpdateRepository _repository;
        private readonly IIdeaRepository _ideaRepository;
        public StartupUpdateController(IStartupUpdateRepository repository, IIdeaRepository ideaRepository)
        {
            _repository = repository;
            _ideaRepository = ideaRepository;
        }

        public async Task<IActionResult> Create(int ideaId)
        {
            var vm = new StartupUpdateCreateVM
            {
                IdeaId = ideaId
            };

            var idea = await _ideaRepository.GetIdeaByIdAsync(ideaId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId != idea.UserId)
            {
                return Unauthorized();
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StartupUpdateCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                var idea = await _ideaRepository.GetIdeaByIdAsync(vm.IdeaId);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId != idea.UserId)
                {
                    return Unauthorized();
                }

                StartupUpdate startupUpdate = new StartupUpdate()
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    CreatedDate = DateTime.Now,
                    IdeaId = vm.IdeaId
                };

                await _repository.AddUpdateAsync(startupUpdate);

                return RedirectToAction("Details", "Idea", new { id = vm.IdeaId });
            }
            return View(vm);
        }


    }
}
