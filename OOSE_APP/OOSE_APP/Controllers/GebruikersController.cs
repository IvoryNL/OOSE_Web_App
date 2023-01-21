using Logic.Models.Constants;
using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using OOSE_APP.Models;
using HttpResponseException = System.Web.Http.HttpResponseException;
using Logic.Models.Dto;
using Logic.Services;
using Presentation.ViewModels.Gebruikers;

namespace Presentation.Controllers
{
    public class GebruikersController : BaseController
    {
        private readonly IGebruikerService _gebruikerService;
        private readonly IRolService _rolService;
        private readonly IOpleidingService _opleidingService;
        private readonly IKlasService _klasService;

        public GebruikersController(
            IGebruikerService gebruikerService,
            IRolService rolService,
            IOpleidingService opleidingsProfielService,
            IKlasService klasService)
        {
            _gebruikerService = gebruikerService;
            _rolService = rolService;
            _opleidingService = opleidingsProfielService;
            _klasService = klasService;
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
            var gebruikers = Enumerable.Empty<VolledigeGebruikerModelDto>();
            try
            {
                if (User.IsInRole(Rollen.ADMIN))
                {
                    gebruikers = await _gebruikerService.GetAllGebruikers(jwtToken);
                }
                else
                {
                    gebruikers = await _gebruikerService.GetAllStudenten(jwtToken);
                }
            }
            catch (HttpResponseException ex)
            {
                return await HandleException(ex);
            }

            return View("Index", gebruikers);
        }

        [HttpGet]
        public async Task<IActionResult> BekijkGebruiker(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }
            
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            if (!IsWerknemer())
            {
                return Unauthorized();
            }

            BaseGebruikerModelDto gebruiker;
            try
            {
                gebruiker = await _gebruikerService.GetGebruikerById(id, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                return await HandleException(ex);
            }

            return View(gebruiker);
        }

        [HttpGet]
        public async Task<IActionResult> WijzigGebruiker(int id, string actionType)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            if (!IsWerknemer())
            {
                return Unauthorized();
            }

            var viewModel = new GebruikerViewModel();
            try
            {
                viewModel.Gebruiker = await _gebruikerService.GetGebruikerById(id, jwtToken);
                viewModel.Rollen = await _rolService.GetAllRollen(jwtToken);
                viewModel.Opleidingen = await _opleidingService.GetAllOpleidingen(jwtToken);
                viewModel.Klassen = await _klasService.GetAllKlassen(jwtToken);
                if (viewModel.Gebruiker.OpleidingId != null)
                {
                    viewModel.Klassen = await _klasService.GetKlasenByOpleidingId((int)viewModel.Gebruiker.OpleidingId, jwtToken);
                }
            }
            catch (HttpResponseException ex)
            {
                return await HandleException(ex);
            }

            var viewName = GetViewByActionType(actionType);
            return View(viewName, viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigGebruiker(GebruikerViewModel gebruikerViewModel)
        {
            var gebruiker = MapGebruikerViewModelGebruikerToDtoMode(gebruikerViewModel);
            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);

            try
            {
               await _gebruikerService.UpdateGebruiker(gebruiker.Id, gebruiker, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                return await HandleException(ex);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> VoegGebruikerToeAanKlas(GebruikerViewModel gebruikerViewModel)
        {
            var gebruiker = MapGebruikerViewModelGebruikerToDtoMode(gebruikerViewModel);
            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            if (!string.IsNullOrEmpty(gebruikerViewModel.GeselecteerdeKlasId))
            {
                var klas = await _klasService.GetKlasById(int.Parse(gebruikerViewModel.GeselecteerdeKlasId), jwtToken);
                gebruiker.Klassen.Add(klas);
            }

            try
            {
                await _gebruikerService.AddGebruikerToKlas(gebruiker.Id, gebruiker, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                return await HandleException(ex);
            }

            return RedirectToAction("Index");
        }

        private async Task<ViewResult> HandleException(HttpResponseException ex)
        {
            var message = await ex.Response.Content.ReadAsStringAsync();
            var errorViewModel = new ErrorViewModel { StatusCode = ex.Response.StatusCode, ErrorMessage = message };
            return View("Error", errorViewModel);
        }

        private VolledigeGebruikerModelDto MapGebruikerViewModelGebruikerToDtoMode(GebruikerViewModel gebruikerViewModel)
        {
            return new VolledigeGebruikerModelDto
            {
                Id = gebruikerViewModel.Gebruiker.Id,
                Voornaam = gebruikerViewModel.Gebruiker.Voornaam,
                Achternaam = gebruikerViewModel.Gebruiker.Achternaam,
                Email = gebruikerViewModel.Gebruiker.Email,
                Code = gebruikerViewModel.Gebruiker.Code,
                OpleidingsprofielId = gebruikerViewModel.Gebruiker.OpleidingsprofielId,
                Beoordelingsmodellen = gebruikerViewModel.Gebruiker.Beoordelingsmodellen,
                TentamensVanStudent = gebruikerViewModel.Gebruiker.TentamensVanStudent,
                Klassen = gebruikerViewModel.Gebruiker.Klassen,
                RolId = !string.IsNullOrEmpty(gebruikerViewModel.GeselecteerdeRolId) ? int.Parse(gebruikerViewModel.GeselecteerdeRolId) : gebruikerViewModel.Gebruiker.RolId,
                OpleidingId = !string.IsNullOrEmpty(gebruikerViewModel.GeselecteerdeOpleidingId) ? int.Parse(gebruikerViewModel.GeselecteerdeOpleidingId) : gebruikerViewModel.Gebruiker.RolId
            };
        }

        private string GetViewByActionType(string actionType)
        {
            var viewName = string.Empty;

            if (actionType == WijzigGebruikerActionType.ROL)
            {
                viewName = "WijzigGebruikerRol";
            }
            else if (actionType == WijzigGebruikerActionType.OPLEIDING)
            {
                viewName = "WijzigGebruikerOpleiding";
            }
            else if (actionType == WijzigGebruikerActionType.KLAS)
            {
                viewName = "WijzigGebruikerKlas";
            }

            return viewName;
        }
    }
}
