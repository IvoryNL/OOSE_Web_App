using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using Presentation.ViewModels.Beoordelingsmodellen;
using HttpResponseException = System.Web.Http.HttpResponseException;

namespace Presentation.Controllers
{
    public class BeoordelingsmodellenController : BaseController
    {
        private readonly ITentamenService _tentamenService;
        private readonly IBeoordelingsmodelService _beoordelingsmodelService;
        private readonly IBeoordelingsonderdeelService _beoordelingsonderdeelService;
        private readonly IBeoordelingscriteriaService _beoordelingscriteriaService;
        private readonly IBeoordelingsdimensieService _beoordelingsdimensieService;
        private readonly IGebruikerService _gebruikerService;

        public BeoordelingsmodellenController(
            ITentamenService tentamenService, 
            IBeoordelingsmodelService beoordelingsmodelService,
            IBeoordelingsonderdeelService beoordelingsonderdeelService,
            IBeoordelingscriteriaService beoordelingsCriteriaService,
            IBeoordelingsdimensieService beoordelingsdimensieService,
            IGebruikerService gebruikerService)
        {
            _tentamenService = tentamenService;
            _beoordelingsmodelService = beoordelingsmodelService;
            _beoordelingsonderdeelService = beoordelingsonderdeelService;
            _beoordelingscriteriaService = beoordelingsCriteriaService;
            _beoordelingsdimensieService = beoordelingsdimensieService;
            _gebruikerService = gebruikerService;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var beoordelingsmodellen = await _beoordelingsmodelService.GetAllBeoordelingsmodellen(jwtToken);

            return View(beoordelingsmodellen);
        }

        [HttpGet]
        public async Task<IActionResult> BeoordelingsmodelDetails(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var beoordelingsmodel = await _beoordelingsmodelService.GetBeoordelingsmodelById(id, jwtToken);

            return View(beoordelingsmodel);
        }

        [HttpGet]
        public async Task<IActionResult> MaakBeoordelingsmodel()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var viewModel = new BeoordelingsmodelViewModel();
            viewModel.Tentamens = await _tentamenService.GetAllTentamens(jwtToken);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MaakBeoordelingsmodel(BeoordelingsmodelViewModel viewModel)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            viewModel.Beoordelingsmodel.TentamenId = int.Parse(viewModel.GeselecteerdeTentamenId);
            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var gebruiker = await GetIngelogdeGebruikerByEmail(_gebruikerService, jwtToken);
            viewModel.Beoordelingsmodel.DocentId = gebruiker.Id;
            
            try
            {
                await _beoordelingsmodelService.CreateBeoordelingsmodel(viewModel.Beoordelingsmodel, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> WijzigBeoordelingsmodel(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var viewModel = new BeoordelingsmodelViewModel();
            viewModel.Beoordelingsmodel = await _beoordelingsmodelService.GetBeoordelingsmodelById(id, jwtToken);
            viewModel.Tentamens = await _tentamenService.GetAllTentamens(jwtToken);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigBeoordelingsmodel(BeoordelingsmodelViewModel viewModel)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var gebruiker = await GetIngelogdeGebruikerByEmail(_gebruikerService, jwtToken);
            viewModel.Beoordelingsmodel.DocentId = gebruiker.Id;
            viewModel.Beoordelingsmodel.TentamenId = int.Parse(viewModel.GeselecteerdeTentamenId);

            try
            {
                await _beoordelingsmodelService.UpdateBeoordelingsmodel(viewModel.Beoordelingsmodel.Id, viewModel.Beoordelingsmodel, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> BeoordelingsonderdeelDetails(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var beoordelingsonderdeel = await _beoordelingsonderdeelService.GetBeoordelingsonderdeelById(id, jwtToken);

            return View(beoordelingsonderdeel);
        }

        [HttpGet]
        public async Task<IActionResult> MaakBeoordelingsonderdeel(int beoordelingsmodelId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var beoordelingsonderdeel = new Beoordelingsonderdeel();
            beoordelingsonderdeel.BeoordelingsmodelId = beoordelingsmodelId;

            return View(beoordelingsonderdeel);
        }

        [HttpPost]
        public async Task<IActionResult> MaakBeoordelingsonderdeel(Beoordelingsonderdeel beoordelingsonderdeel)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _beoordelingsonderdeelService.CreateBeoordelingsonderdeel(beoordelingsonderdeel, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(beoordelingsonderdeel);
            }

            return RedirectToAction("BeoordelingsmodelDetails", new { id = beoordelingsonderdeel.BeoordelingsmodelId });
        }

        [HttpGet]
        public async Task<IActionResult> WijzigBeoordelingsonderdeel(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var beoordelingsonderdeel = await _beoordelingsonderdeelService.GetBeoordelingsonderdeelById(id, jwtToken);

            return View(beoordelingsonderdeel);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigBeoordelingsonderdeel(Beoordelingsonderdeel beoordelingsonderdeel)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _beoordelingsonderdeelService.UpdateBeoordelingsonderdeel(beoordelingsonderdeel.Id, beoordelingsonderdeel, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(beoordelingsonderdeel);
            }

            return RedirectToAction("BeoordelingsmodelDetails", new { id = beoordelingsonderdeel.BeoordelingsmodelId });
        }

        [HttpGet]
        public async Task<IActionResult> BeoordelingscriteriaDetails(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var beoordelingscriteria = await _beoordelingscriteriaService.GetBeoordelingscriteriaById(id, jwtToken);

            return View(beoordelingscriteria);
        }

        [HttpGet]
        public async Task<IActionResult> MaakBeoordelingscriteria(int beoordelingsonderdeelId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var beoordelingscriteria = new Beoordelingscriteria();
            beoordelingscriteria.BeoordelingsonderdeelId = beoordelingsonderdeelId;

            return View(beoordelingscriteria);
        }

        [HttpPost]
        public async Task<IActionResult> MaakBeoordelingscriteria(Beoordelingscriteria beoordelingscriteria)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _beoordelingscriteriaService.CreateBeoordelingscriteria(beoordelingscriteria, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(beoordelingscriteria);
            }

            return RedirectToAction("BeoordelingsonderdeelDetails", new { id = beoordelingscriteria.BeoordelingsonderdeelId });
        }

        [HttpGet]
        public async Task<IActionResult> WijzigBeoordelingscriteria(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var beoordelingscriteria = await _beoordelingscriteriaService.GetBeoordelingscriteriaById(id, jwtToken);

            return View(beoordelingscriteria);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigBeoordelingscriteria(Beoordelingscriteria beoordelingscriteria)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _beoordelingscriteriaService.UpdateBeoordelingscriteria(beoordelingscriteria.Id, beoordelingscriteria, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(beoordelingscriteria);
            }

            return RedirectToAction("BeoordelingsonderdeelDetails", new { id = beoordelingscriteria.BeoordelingsonderdeelId });
        }

        [HttpGet]
        public async Task<IActionResult> BeoordelingsdimensieDetails(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var beoordelingsdimensie = await _beoordelingsdimensieService.GetBeoordelingsdimensieById(id, jwtToken);

            return View(beoordelingsdimensie);
        }

        [HttpGet]
        public async Task<IActionResult> MaakBeoordelingsdimensie(int beoordelingscriteriaId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var beoordelingsdimensie = new Beoordelingsdimensie();
            beoordelingsdimensie.BeoordelingscriteriaId = beoordelingscriteriaId;

            return View(beoordelingsdimensie);
        }

        [HttpPost]
        public async Task<IActionResult> MaakBeoordelingsdimensie(Beoordelingsdimensie beoordelingsdimensie)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _beoordelingsdimensieService.CreateBeoordelingsdimensie(beoordelingsdimensie, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(beoordelingsdimensie);
            }

            return RedirectToAction("BeoordelingscriteriaDetails", new { id = beoordelingsdimensie.BeoordelingscriteriaId });
        }

        [HttpGet]
        public async Task<IActionResult> WijzigBeoordelingsdimensie(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var beoordelingsdimensie = await _beoordelingsdimensieService.GetBeoordelingsdimensieById(id, jwtToken);

            return View(beoordelingsdimensie);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigBeoordelingsdimensie(Beoordelingsdimensie beoordelingsdimensie)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _beoordelingsdimensieService.UpdateBeoordelingsdimensie(beoordelingsdimensie.Id, beoordelingsdimensie, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(beoordelingsdimensie);
            }

            return RedirectToAction("BeoordelingscriteriaDetails", new { id = beoordelingsdimensie.BeoordelingscriteriaId });
        }
    }
}
