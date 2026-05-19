using Idea_Fundu.Interfaces;
using Idea_Fundu.Models;
using Idea_Fundu.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Idea_Fundu.Controllers
{
    public class StartupUpdateController : Controller
    {
        private readonly IStartupUpdateRepository _repository;
        public StartupUpdateController(IStartupUpdateRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Create(int ideaId)
        {
            var vm = new StartupUpdateCreateVM
            {
                IdeaId = ideaId
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StartupUpdateCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                StartupUpdate startupUpdate = new StartupUpdate()
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    CreatedDate = DateTime.Now,
                    IdeaId = vm.IdeaId
                };

                await _repository.AddUpdateAsync(startupUpdate);

                RedirectToAction("Details", "Idea", new { id = vm.IdeaId });
            }
            return View(vm);
        }


    }
}
