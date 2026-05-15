using Idea_Fundu.Interfaces;
using Idea_Fundu.Models;
using Idea_Fundu.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Idea_Fundu.Controllers
{
    public class IdeaController : Controller
    {
        private readonly IIdeaRepository _ideaRepository;

        public IdeaController(IIdeaRepository ideaRepository)
        {
            _ideaRepository = ideaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ideas = await _ideaRepository.GetAllIdeasAsync();
            return View(ideas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdeaCreateVM ideaCreateVM)
        {
            if (ModelState.IsValid)
            {
                Idea idea = new Idea
                {
                    Title = ideaCreateVM.Title,
                    Description = ideaCreateVM.Description,
                    RequiredFund = ideaCreateVM.RequiredFund,
                    Category = ideaCreateVM.Category,
                    RiskLevel = ideaCreateVM.RiskLevel,
                    Restrictions = ideaCreateVM.Restrictions,
                };
                await _ideaRepository.AddIdeaAsync(idea);
                return RedirectToAction("Index");
            }
            return View(ideaCreateVM);
        }
    }
}
