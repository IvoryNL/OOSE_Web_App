using Logic.Enums;
using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using Presentation.ViewModels.Lessen;

namespace Presentation.Controllers
{
    public class LessenController : BaseController
    {
        private readonly ILesService _lesService;
        private readonly ILesmateriaalService _lesmateriaalService;
        private readonly ILeeruitkomstService _leeruitkomstService;
        private readonly IOpleidingService _opleidingService;
        private readonly IOnderwijsuitvoeringService _onderwijsuitvoeringService;

        public LessenController(ILesService lesService, ILesmateriaalService lesmateriaalService, 
            ILeeruitkomstService leeuitkomstService, IOpleidingService opleidingService,
            IOnderwijsuitvoeringService onderwijsuitvoeringService)
        {
            _lesService = lesService;
            _lesmateriaalService = lesmateriaalService;
            _leeruitkomstService = leeuitkomstService;
            _opleidingService = opleidingService;
            _onderwijsuitvoeringService = onderwijsuitvoeringService;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var lessen = await _lesService.GetAllLessen(jwtToken);

            return View(lessen);
        }

        [HttpGet]
        public async Task<IActionResult> Overzicht(int id, ControllerActionTypes actionType) 
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var les = await _lesService.GetLesById(id, jwtToken);
            var viewName = string.Empty;

            if (actionType == ControllerActionTypes.Leeruitkomsten)
            {
                viewName = "LeeruitkomstenOverzicht";
            }
            else if (actionType == ControllerActionTypes.Lesmaterialen)
            {
                viewName = "LesmaterialenOverzicht";
            }
            else if (actionType == ControllerActionTypes.Planningen)
            {
                viewName = "Planningoverzicht";
            }

            return View(viewName, les);
        }

        [HttpGet]
        public async Task<IActionResult> KoppelLesmateriaalAanLes(int lesId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var lesMaterialen = await _lesmateriaalService.GetAllLesmaterialen(jwtToken);

            var viewModel = new LessenViewModel();
            viewModel.LesId = lesId;
            viewModel.Lesmaterialen = lesMaterialen;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> KoppelLesmateriaalAanLes(LessenViewModel lessenViewModel)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var les = await _lesService.GetLesById(lessenViewModel.LesId, jwtToken);
            var lesmateriaal = await _lesmateriaalService.GetLesmateriaalById(int.Parse(lessenViewModel.GeselecteerdeLesmateriaalId), jwtToken);
            les.Lesmaterialen.Add(lesmateriaal);

            await _lesService.KoppelLesmateriaalAanLes(les.Id, les, jwtToken);
            les = await _lesService.GetLesById(lessenViewModel.LesId, jwtToken);

            return View("LesmaterialenOverzicht", les);
        }

        [HttpGet]
        public async Task<IActionResult> OntkoppelLesmateriaalVanLes(int lesId, int lesmateriaalId)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            await _lesService.OntkoppelLesmateriaalVanLes(lesId, lesmateriaalId, jwtToken);

            var les = await _lesService.GetLesById(lesId, jwtToken);
            return View("LesmaterialenOverzicht", les);
        }

        [HttpGet]
        public async Task<IActionResult> KoppelLeeruitkomstAanLes(int lesId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var leeruitkomsten = await _leeruitkomstService.GetAllLeeruitkomsten(jwtToken);
            var opleidingen = await _opleidingService.GetAllOpleidingen(jwtToken);

            var viewModel = new LessenViewModel();
            viewModel.LesId = lesId;
            viewModel.Leeruitkomsten = leeruitkomsten;
            viewModel.Opleidingen = opleidingen;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> KoppelLeeruitkomstAanLes(LessenViewModel lessenViewModel)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var les = await _lesService.GetLesById(lessenViewModel.LesId, jwtToken);
            var leeruitkomst = await _leeruitkomstService.GetLeeruitkomstById(int.Parse(lessenViewModel.GeselecteerdeLeeruitkomstId), jwtToken);
            les.Leeruitkomsten.Add(leeruitkomst);

            await _lesService.KoppelLeeruitkomstAanLes(les.Id, les, jwtToken);
            les = await _lesService.GetLesById(lessenViewModel.LesId, jwtToken);

            return View("LeeruitkomstenOverzicht", les);
        }

        [HttpGet]
        public async Task<IActionResult> OntkoppelLeeruitkomstVanLes(int lesId, int leeruitkomstId)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            await _lesService.OntkoppelLeeruitkomstVanLes(lesId, leeruitkomstId, jwtToken);

            var les = await _lesService.GetLesById(lesId, jwtToken);
            return View("LeeruitkomstenOverzicht", les);
        }

        [HttpPost]
        public async Task<IActionResult> FilterLeeruitkomstenViaOpleiding(LessenViewModel lessenViewModel)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            lessenViewModel.Leeruitkomsten = await _leeruitkomstService.GetLeeruitkomstenByOpleidingId(int.Parse(lessenViewModel.GeselecteerdeOpleidingId), jwtToken);
            lessenViewModel.Opleidingen = await _opleidingService.GetAllOpleidingen(jwtToken);

            return View("KoppelLeeruitkomstAanLes", lessenViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> InplannenLes(int lesId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var viewModel = new LessenViewModel();
            viewModel.LesId = lesId;
            viewModel.Onderwijsuitvoeringen = await _onderwijsuitvoeringService.GetAllOnderwijsuitvoeringen(jwtToken);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> InplannenLes(LessenViewModel lessenViewModel)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var les = await _lesService.GetLesById(lessenViewModel.LesId, jwtToken);
            les.Planningen.Clear();
            les.Planningen.Add(new Planning(lessenViewModel.Datum, lessenViewModel.Weeknummer, int.Parse(lessenViewModel.GeselecteerdeOnderwijsuitvoeringId)));

            await _lesService.InplannenLes(les.Id, les, jwtToken);
            les = await _lesService.GetLesById(lessenViewModel.LesId, jwtToken);

            return View("Planningoverzicht", les);
        }

        [HttpGet]
        public async Task<IActionResult> VerwijderPlanningVanLes(int lesId, int planningId)
        {
            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            await _lesService.VerwijderPlanningVanLes(lesId, planningId, jwtToken);

            var les = await _lesService.GetLesById(lesId, jwtToken);
            return View("Planningoverzicht", les);
        }
    }
}
