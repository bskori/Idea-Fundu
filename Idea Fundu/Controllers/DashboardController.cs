using Idea_Fundu.Interfaces;
using Idea_Fundu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Idea_Fundu.Controllers
{
    //[Authorize]
    public class DashboardController : Controller
    {
        private readonly IIdeaRepository _ideaRepository;
        private readonly IInvestmentRepository _investmentRepository;

        public DashboardController(IIdeaRepository ideaRepository, IInvestmentRepository investmentRepository)
        {
            _ideaRepository = ideaRepository;
            _investmentRepository = investmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ideas = await _ideaRepository.GetIdeasByUserAsync(userId);
            var investments = await _investmentRepository.GetInvestmentByUserAsync(userId);

            DashboardVM vm = new DashboardVM
            {
                TotalIdeas = ideas.Count(),
                TotalInvestments = investments.Count(),
                TotalAmountInvested = investments.Sum(x => x.Amount),
                RecentIdeas = ideas.OrderByDescending(x => x.CreatedDate).Take(5),
                RecentInvestments = investments.OrderByDescending(x => x.InvestmentDate).Take(5)
            };

            return View(vm);
        }
    }
}
