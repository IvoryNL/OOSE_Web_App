using Logic.Models.Dto;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Presentation.Helpers;
using Presentation.ViewModels.Opleidingen;

namespace Presentation.Controllers
{
    public class OpleidingenController : BaseController
    {
        private readonly IGebruikerService _gebruikerService;
        private readonly IOpleidingsprofielService _opleidingsprofielService;

        public OpleidingenController(IGebruikerService gebruikerService, IOpleidingsprofielService opleidingsprofielService)
        {
            _gebruikerService = gebruikerService;
            _opleidingsprofielService = opleidingsprofielService;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var gebruiker = await GetIngelogdeGebruikerByEmail(jwtToken);

            return View(gebruiker);
        }

        [HttpGet]
        public async Task<IActionResult> WijzigOpleidingsprofiel()
        {
            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var gebruiker = await GetIngelogdeGebruikerByEmail(jwtToken);
            var viewModel = new OpleidingsprofielViewModel();
            viewModel.OpleidingVanStudent = gebruiker.Opleiding;
            viewModel.OpleidingsprofielVanStudent = gebruiker.Opleidingsprofiel;
            viewModel.Opleidingsprofielen = await _opleidingsprofielService.GetAllOpleidingsprofielenByOpleidingId((int)gebruiker.OpleidingId!, jwtToken);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigOpleidingsprofiel(OpleidingsprofielViewModel opleidingsprofielViewModel)
        {
            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var gebruiker = await GetIngelogdeGebruikerByEmail(jwtToken);
            gebruiker.OpleidingsprofielId = int.Parse(opleidingsprofielViewModel.GeselecteerdeOpleidingsprofielId);

            await _gebruikerService.UpdateGebruiker(gebruiker.Id, gebruiker, jwtToken);

            return RedirectToAction("Index");
        }

        private async Task<VolledigeGebruikerModelDto> GetIngelogdeGebruikerByEmail(string jwtToken)
        {
            var ingelogdeGebruikerEmail = GetLoggedInUserEmail();
            return await _gebruikerService.GetGebruikerByEmail(ingelogdeGebruikerEmail, jwtToken);
        }
    }
}
