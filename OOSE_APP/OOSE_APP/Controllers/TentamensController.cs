using Logic.Constants;
using Logic.Enums;
using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using Presentation.ViewModels.Tentamens;

namespace Presentation.Controllers
{
    public class TentamensController : BaseController
    {
        private readonly ITentamenService _tentamenService;
        private readonly ILeeruitkomstService _leeruitkomstService;
        private readonly IOpleidingService _opleidingService;
        private readonly IOnderwijsuitvoeringService _onderwijsuitvoeringService;
        private readonly IGebruikerService _gebruikerService;
        private readonly IToetsinschrijvingService _toetsinschrijvingService;

        public TentamensController(
            ITentamenService tentamenService, 
            ILeeruitkomstService leeuitkomstService, 
            IOpleidingService opleidingService,
            IOnderwijsuitvoeringService onderwijsuitvoeringService,
            IGebruikerService gebruikerService,
            IToetsinschrijvingService toetsinschrijvingService)
        {
            _tentamenService = tentamenService;
            _leeruitkomstService = leeuitkomstService;
            _opleidingService = opleidingService;
            _onderwijsuitvoeringService = onderwijsuitvoeringService;
            _gebruikerService = gebruikerService;
            _toetsinschrijvingService = toetsinschrijvingService;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var viewModel = new TentamenOverzichtViewModel();
            viewModel.Gebruiker = await _gebruikerService.GetGebruikerByEmail(GetLoggedInUserEmail(), jwtToken);
            viewModel.Tentamens = User.IsInRole(Rollen.DOCENT) ? 
                await _tentamenService.GetAllTentamens(jwtToken) : 
                await _tentamenService.GetAllTentamensVanOnderwijsuitvoeringStudent(viewModel.Gebruiker.Id, jwtToken);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Overzicht(int id, ControllerActionTypes actionType)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var tentamen = await _tentamenService.GetTentamenById(id, jwtToken);
            var viewName = string.Empty;

            if (actionType == ControllerActionTypes.Leeruitkomsten)
            {
                viewName = "LeeruitkomstenOverzicht";
            }
            else if (actionType == ControllerActionTypes.Planningen)
            {
                viewName = "Planningoverzicht";
            }

            return View(viewName, tentamen);
        }

        [HttpGet]
        public async Task<IActionResult> KoppelLeeruitkomstAanTentamen(int tentamenId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var leeruitkomsten = await _leeruitkomstService.GetAllLeeruitkomsten(jwtToken);
            var opleidingen = await _opleidingService.GetAllOpleidingen(jwtToken);

            var viewModel = new TentamensViewModel();
            viewModel.TentamenId = tentamenId;
            viewModel.Leeruitkomsten = leeruitkomsten;
            viewModel.Opleidingen = opleidingen;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> KoppelLeeruitkomstAanTentamen(TentamensViewModel tentamensViewModel)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var tentamen = await _tentamenService.GetTentamenById(tentamensViewModel.TentamenId, jwtToken);
            var leeruitkomst = await _leeruitkomstService.GetLeeruitkomstById(int.Parse(tentamensViewModel.GeselecteerdeLeeruitkomstId), jwtToken);
            tentamen.Leeruitkomsten.Add(leeruitkomst);

            await _tentamenService.KoppelLeeruitkomstAanTentamen(tentamen.Id, tentamen, jwtToken);
            tentamen = await _tentamenService.GetTentamenById(tentamensViewModel.TentamenId, jwtToken);

            return View("LeeruitkomstenOverzicht", tentamen);
        }

        [HttpGet]
        public async Task<IActionResult> OntkoppelLeeruitkomstVanTentamen(int tentamenId, int leeruitkomstId)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            await _tentamenService.OntkoppelLeeruitkomstVanTentamen(tentamenId, leeruitkomstId, jwtToken);

            var tentamen = await _tentamenService.GetTentamenById(tentamenId, jwtToken);
            return View("LeeruitkomstenOverzicht", tentamen);
        }

        [HttpPost]
        public async Task<IActionResult> FilterLeeruitkomstenViaOpleiding(TentamensViewModel tentamenViewModel)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            tentamenViewModel.Leeruitkomsten = await _leeruitkomstService.GetLeeruitkomstenByOpleidingId(int.Parse(tentamenViewModel.GeselecteerdeOpleidingId), jwtToken);
            tentamenViewModel.Opleidingen = await _opleidingService.GetAllOpleidingen(jwtToken);

            return View("KoppelLeeruitkomstAanTentamen", tentamenViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> InplannenTentamen(int tentamenId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var viewModel = new TentamensViewModel();
            viewModel.TentamenId = tentamenId;
            viewModel.Onderwijsuitvoeringen = await _onderwijsuitvoeringService.GetAllOnderwijsuitvoeringen(jwtToken);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> InplannenTentamen(TentamensViewModel tentamensViewModel)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var tentamen = await _tentamenService.GetTentamenById(tentamensViewModel.TentamenId, jwtToken);
            tentamen.Planningen.Clear();
            tentamen.Planningen.Add(new Planning(tentamensViewModel.Datum, tentamensViewModel.Weeknummer, int.Parse(tentamensViewModel.GeselecteerdeOnderwijsuitvoeringId)));

            await _tentamenService.InplannenTentamen(tentamen.Id, tentamen, jwtToken);
            tentamen = await _tentamenService.GetTentamenById(tentamensViewModel.TentamenId, jwtToken);

            return View("Planningoverzicht", tentamen);
        }

        [HttpGet]
        public async Task<IActionResult> VerwijderPlanningVanTentamen(int tentamenId, int planningId)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            await _tentamenService.VerwijderPlanningVanTentamen(tentamenId, planningId, jwtToken);

            var tentamen = await _tentamenService.GetTentamenById(tentamenId, jwtToken);
            return View("Planningoverzicht", tentamen);
        }

        [HttpGet]
        public async Task<IActionResult> InschrijvenVoorTentamen(int tentamenId, int planningId)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var student = await _gebruikerService.GetGebruikerByEmail(GetLoggedInUserEmail(), jwtToken);
            var toetsinschrijving = new Toetsinschrijving(student.Id, tentamenId, planningId);

            await _toetsinschrijvingService.CreateToetsinschrijving(toetsinschrijving, jwtToken);

            return RedirectToAction("Index");
        }
    }
}
