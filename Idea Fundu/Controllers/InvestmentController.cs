using Idea_Fundu.Interfaces;
using Idea_Fundu.Models;
using Idea_Fundu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Idea_Fundu.Controllers
{
    public class InvestmentController : Controller
    {
        private readonly IInvestmentRepository _investmentRepository;

        public InvestmentController(IInvestmentRepository investmentRepository)
        {
            _investmentRepository = investmentRepository;
        }

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

                return RedirectToAction("Index", "Idea");

            }
            return View(vm);
        }
    }
}
