using AspNetCoreGeneratedDocument;
using Idea_Fundu.Interfaces;
using Idea_Fundu.Models;
using Idea_Fundu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace Idea_Fundu.Controllers
{
    public class IdeaController : Controller
    {
        private readonly IIdeaRepository _ideaRepository;
        private readonly IStartupUpdateRepository _startupUpdateRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IdeaController(IIdeaRepository ideaRepository, IStartupUpdateRepository startupUpdateRepository, ICommentRepository commentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _ideaRepository = ideaRepository;
            _commentRepository = commentRepository;
            _startupUpdateRepository = startupUpdateRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string searchTerm, string category, int page=1)
        {
            var ideas = await _ideaRepository.SearchIdeasAsync(searchTerm, category);

            int pageSize = 6;

            var pagedIdeas = ideas.Skip((page - 1)).Take(pageSize).ToList();

            ViewBag.Categories = new List<string>
            {
                "AI & Recruitment",
                "AgriTech",
                "HealthTech",
                "Green Energy",
                "EdTech"
            };

            ViewBag.SearchTerm = searchTerm;
            ViewBag.Category = category;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(ideas.Count() / (double)pageSize);

            return View(ideas);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(new List<string>
            {
                "AI & Recruitment",
                "AgriTech",
                "HealthTech",
                "Green Energy",
                "EdTech"
            });
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(IdeaCreateVM ideaCreateVM)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if(ideaCreateVM.ImageFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                    fileName = Guid.NewGuid().ToString() + "_" + ideaCreateVM.ImageFile.FileName;

                    string filePath = Path.Combine(uploadFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ideaCreateVM.ImageFile.CopyToAsync(fileStream);      
                    }
                }

                Idea idea = new Idea
                {
                    Title = ideaCreateVM.Title,
                    Description = ideaCreateVM.Description,
                    RequiredFund = ideaCreateVM.RequiredFund,
                    Category = ideaCreateVM.Category,
                    RiskLevel = ideaCreateVM.RiskLevel,
                    Restrictions = ideaCreateVM.Restrictions,
                    ImageUrl = fileName,
                    Status = "Pending",
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)

                };
                await _ideaRepository.AddIdeaAsync(idea);
                return RedirectToAction("MyIdeas");
            }
            ViewBag.Categories = new SelectList(new List<string>
            {
                "AI & Recruitment",
                "AgriTech",
                "HealthTech",
                "Green Energy",
                "EdTech"
            });
            return View(ideaCreateVM);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var idea = await _ideaRepository.GetIdeaDetailsByIdAsync(id);

            if (idea == null)
            {
                return NotFound();
            }

            // Total Investment
            var totalInvestment = await _ideaRepository.GetTotalInvestmentAsync(id);

            // Progress %
            decimal progress = 0;

            if (idea.RequiredFund > 0)
            {
                progress = (totalInvestment / idea.RequiredFund) * 100;
            }

            // Remaining Amount
            var remainingAmount = idea.RequiredFund - totalInvestment;

            if (remainingAmount < 0)
            {
                remainingAmount = 0;
            }

            // Startup Updates
            var updates = await _startupUpdateRepository.GetUpdatesByIdAsync(id);

            // Comments
            var comments = await _commentRepository.GetCommentsByIdeaAsync(id);

            // ViewBag Data
            ViewBag.TotalInvestment = totalInvestment;

            ViewBag.Progress = progress;

            ViewBag.RemainingAmount = remainingAmount;

            ViewBag.Updates = updates;

            ViewBag.Comments = comments;



            return View(idea);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var idea = await _ideaRepository.GetIdeaByIdAsync(id);

            if(idea == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId != idea.UserId)
            {
                return Unauthorized();
            }

            var vm = new IdeaCreateVM
            {
                Id = idea.Id,
                Title = idea.Title,
                Description = idea.Description,
                Category = idea.Category,
                RequiredFund = idea.RequiredFund,
                Restrictions = idea.Restrictions,
                RiskLevel = idea.RiskLevel,
                Status = idea.Status,
                ImageUrl = idea.ImageUrl
            };

            ViewBag.Categories = new SelectList(new List<string>
            {
                "AI & Recruitment",
                "AgriTech",
                "HealthTech",
                "Green Energy",
                "EdTech"
            });

            return View(vm);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(IdeaCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                var idea = await _ideaRepository.GetIdeaByIdAsync(vm.Id);

                if(idea == null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if(userId != idea.UserId)
                {
                    return Unauthorized();
                }

                idea.Title = vm.Title;
                idea.Description = vm.Description;
                idea.Category = vm.Category;
                idea.RequiredFund = vm.RequiredFund;
                idea.Restrictions = vm.Restrictions;
                idea.RiskLevel = vm.RiskLevel;
                idea.Status = vm.Status;

                string fileName = vm.ImageUrl;

                if(vm.ImageFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                    fileName = Guid.NewGuid().ToString() + "_" + vm.ImageFile.FileName;

                    string filePath = Path.Combine(uploadFolder, fileName);

                    using(var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await vm.ImageFile.CopyToAsync(fileStream);
                    }

                }

                idea.ImageUrl = fileName;

                await _ideaRepository.UpdateIdeaAsync(idea);

                return RedirectToAction("MyIdeas");
            }

            ViewBag.Categories = new SelectList(new List<string>
            {
                "AI & Recruitment",
                "AgriTech",
                "HealthTech",
                "Green Energy",
                "EdTech"
            });

            return View(vm);
            
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var idea = await _ideaRepository.GetIdeaByIdAsync(id);

            if(idea == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(idea.UserId != userId)
            {
                return Unauthorized();
            }

            await _ideaRepository.DeleteIdeaAsync(id);

            return RedirectToAction("MyIdeas");
        }

        [Authorize]
        public async Task<IActionResult> MyIdeas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ideas = await _ideaRepository.GetIdeasByUserAsync(userId);

            return View(ideas);
        }
    }
}
