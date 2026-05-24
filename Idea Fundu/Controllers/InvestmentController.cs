using Idea_Fundu.Interfaces;
using Idea_Fundu.Models;
using Idea_Fundu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Idea_Fundu.Controllers
{
    [Authorize]

    public class InvestmentController : Controller
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public InvestmentController(IInvestmentRepository investmentRepository, UserManager<ApplicationUser> userManager)
        {
            _investmentRepository = investmentRepository;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Create(int ideaId)
        {
            var vm = new InvestmentCreateVM
            {
                IdeaId = ideaId
            };

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(InvestmentCreateVM vm)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user.RoleType != "Investor")
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {

                var investment = new Investment
                {
                    IdeaId = vm.IdeaId,
                    Amount = vm.Amount,
                    Suggestions = vm.Suggestions,
                    InvestmentDate = DateTime.Now,
                    InvestorId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                await _investmentRepository.AddInvestmentAsync(investment);

                return RedirectToAction("MyInvestments");

            }
            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> MyInvestments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var investments = await _investmentRepository.GetInvestmentByUserAsync(userId);

            return View(investments);
        }
    }
}
