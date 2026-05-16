using AspNetCoreGeneratedDocument;
using Idea_Fundu.Interfaces;
using Idea_Fundu.Models;
using Idea_Fundu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Idea_Fundu.Controllers
{
    public class IdeaController : Controller
    {
        private readonly IIdeaRepository _ideaRepository;

        public IdeaController(IIdeaRepository ideaRepository)
        {
            _ideaRepository = ideaRepository;
        }

        public async Task<IActionResult> Index(string searchTerm, string category)
        {
            var ideas = await _ideaRepository.SearchIdeasAsync(searchTerm, category);


            ViewBag.Categories = new List<string>
            {
                "AI & Recruitment",
                "AgriTech",
                "HealthTech",
                "Green Energy",
                "EdTech"
            };

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

        public async Task<IActionResult> Details(int id)
        {
            var idea = await _ideaRepository.GetIdeaDetailsByIdAsync(id);

            if(idea == null)
            {
                return NotFound();
            }

            return View(idea);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var idea = await _ideaRepository.GetIdeaByIdAsync(id);

            if(idea == null)
            {
                return NotFound();
            }

            var vm = new IdeaCreateVM
            {
                Id = idea.Id,
                Title = idea.Title,
                Description = idea.Description,
                Category = idea.Category,
                RequiredFund = idea.RequiredFund,
                Restrictions = idea.Restrictions,
                RiskLevel = idea.RiskLevel
            };

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdeaCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                var idea = await _ideaRepository.GetIdeaByIdAsync(vm.Id);

                if(idea == null)
                {
                    return NotFound();
                }

                idea.Title = vm.Title;
                idea.Description = vm.Description;
                idea.Category = vm.Category;
                idea.RequiredFund = vm.RequiredFund;
                idea.Restrictions = vm.Restrictions;
                idea.RiskLevel = vm.RiskLevel;

                await _ideaRepository.UpdateIdeaAsync(idea);

                return RedirectToAction("Index");
            }

            return View(vm);
            
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _ideaRepository.DeleteIdeaAsync(id);
            return RedirectToAction("Index");
        }
       
    }
}
