using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;

namespace Presentation.Controllers
{
    public class OnderwijsuitvoeringenController : BaseController
    {
        private readonly IOnderwijsuitvoeringService _onderwijsuitvoeringService;
        private readonly IConsistentieCheckService _consistentieCheckService;

        public OnderwijsuitvoeringenController(
            IOnderwijsuitvoeringService onderwijsuitvoeringService, 
            IConsistentieCheckService consistentieCheckService)
        {
            _onderwijsuitvoeringService = onderwijsuitvoeringService;
            _consistentieCheckService = consistentieCheckService;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            if (!IsWerknemer())
            {
                return Unauthorized();
            }

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var onderwijsuitvoeringen = await _onderwijsuitvoeringService.GetAllOnderwijsuitvoeringen(jwtToken);
            return View(onderwijsuitvoeringen);
        }

        [HttpGet]
        public async Task<IActionResult> OnderwijsuitvoeringDetails(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            if (!IsWerknemer())
            {
                return Unauthorized();
            }

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var isConsistent = await _consistentieCheckService.ConsistentieCheckTentamenPlanning(id, jwtToken);
            if (!isConsistent)
            {
                ViewData["Consistentie"] = "Deze onderwijsuitvoering heeft tentamens gepland van een andere onderwijsmodule.";
            }

            var onderwijsuitvoering = await _onderwijsuitvoeringService.GetOnderwijsuitvoeringById(id, jwtToken);

            return View(onderwijsuitvoering);
        }
    }
}
