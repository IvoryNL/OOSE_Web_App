using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using Presentation.ViewModels.Onderwijs;
using HttpResponseException = System.Web.Http.HttpResponseException;

namespace Presentation.Controllers
{
    public class OnderwijsController : BaseController
    {
        private readonly IOnderwijsmoduleService _onderwijsmoduleService;
        private readonly IOnderwijseenheidService _onderwijseenheidService;
        private readonly ILeerdoelService _leerdoelService;
        private readonly IOpleidingService _opleidingService;

        public OnderwijsController(
            IOnderwijsmoduleService onderwijsmoduleService,
            IOnderwijseenheidService onderwijseenheidService,
            ILeerdoelService leerdoelService,
            IOpleidingService opleidingService)
        {
            _opleidingService = opleidingService;
            _onderwijsmoduleService = onderwijsmoduleService;
            _onderwijseenheidService = onderwijseenheidService;
            _leerdoelService = leerdoelService;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var opleidingen = await _opleidingService.GetAllOpleidingen(jwtToken);
                    
            return View(opleidingen);
        }

        [HttpGet]
        public async Task<IActionResult> OverzichtOnderwijsmodules(int opleidingId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var viewModel = new OnderwijsmoduleOverzichtViewModel();
            viewModel.OpleidingId = opleidingId;
            viewModel.Onderwijsmodules =  await _onderwijsmoduleService.GetAllOnderwijsmodulesViaOpleidingId(opleidingId, jwtToken);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> OnderwijsmoduleDetails(int onderwijsmoduleId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var onderwijsModule = await _onderwijsmoduleService.GetOnderwijsmoduleById(onderwijsmoduleId, jwtToken);

            return View(onderwijsModule);
        }

        [HttpGet]
        public ActionResult MaakOnderwijsmodule(int opleidingid)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var onderwijsModule = new Onderwijsmodule();
            onderwijsModule.OpleidingId = opleidingid;
            onderwijsModule.StatusId = 3;

            return View(onderwijsModule);
        }

        [HttpPost]
        public async Task<IActionResult> MaakOnderwijsmodule(Onderwijsmodule onderwijsmodule)
        {
            SetIdentity();

            if (!ModelState.IsValid)
            {
                AddModelStateErrors();
                return View(onderwijsmodule);
            }

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _onderwijsmoduleService.CreateOnderwijsmodule(onderwijsmodule, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(onderwijsmodule);
            }

            return RedirectToAction("OverzichtOnderwijsmodules", new { onderwijsmodule.OpleidingId });
        }

        [HttpGet]
        public async Task<IActionResult> WijzigOnderwijsmodule(int onderwijsmoduleId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var onderwijsModule = await _onderwijsmoduleService.GetOnderwijsmoduleById(onderwijsmoduleId, jwtToken);

            return View(onderwijsModule);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigOnderwijsmodule(Onderwijsmodule onderwijsmodule)
        {
            SetIdentity();

            if (!ModelState.IsValid)
            {
                AddModelStateErrors();
                return View(onderwijsmodule);
            }

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _onderwijsmoduleService.UpdateOnderwijsmodule(onderwijsmodule.Id, onderwijsmodule, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(onderwijsmodule);
            }            

            return RedirectToAction("OverzichtOnderwijsmodules", new { onderwijsmodule.OpleidingId });
        }

        [HttpGet]
        public async Task<IActionResult> OnderwijseenheidDetails(int onderwijseenheidId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var onderwijseenheid = await _onderwijseenheidService.GetOnderwijseenheidById(onderwijseenheidId, jwtToken);

            return View(onderwijseenheid);
        }

        [HttpGet]
        public IActionResult MaakOnderwijseenheid(int onderwijsmoduleId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var viewModel = new OnderwijseenheidViewModel();
            viewModel.OnderwijsmoduleId = onderwijsmoduleId;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MaakOnderwijseenheid(OnderwijseenheidViewModel onderwijseenheidViewModel)
        {
            SetIdentity();

            if (!ModelState.IsValid)
            {
                AddModelStateErrors();
                return View(onderwijseenheidViewModel);
            }

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _onderwijsmoduleService.VoegOnderwijseenheidToe(onderwijseenheidViewModel.OnderwijsmoduleId, onderwijseenheidViewModel.Onderwijseenheid, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(onderwijseenheidViewModel);
            }

            return RedirectToAction("OnderwijsmoduleDetails", new { onderwijseenheidViewModel.OnderwijsmoduleId });
        }

        [HttpGet]
        public async Task<IActionResult> WijzigOnderwijseenheid(int onderwijseenheidId, int onderwijsmoduleId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var viewModel = new OnderwijseenheidViewModel();
            viewModel.OnderwijsmoduleId = onderwijsmoduleId;
            viewModel.Onderwijseenheid =  await _onderwijseenheidService.GetOnderwijseenheidById(onderwijseenheidId, jwtToken);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigOnderwijseenheid(OnderwijseenheidViewModel onderwijseenheidViewModel)
        {
            SetIdentity();

            if (!ModelState.IsValid)
            {
                AddModelStateErrors();
                return View(onderwijseenheidViewModel);
            }

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _onderwijseenheidService.UpdateOnderwijseenheid(onderwijseenheidViewModel.Onderwijseenheid.Id, onderwijseenheidViewModel.Onderwijseenheid, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(onderwijseenheidViewModel);
            }

            return RedirectToAction("OnderwijsmoduleDetails", new { onderwijseenheidViewModel.OnderwijsmoduleId });
        }

        [HttpGet]
        public async Task<IActionResult> LeerdoelDetails(int leerdoelId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var leerdoel = await _leerdoelService.GetLeerdoelById(leerdoelId, jwtToken);

            return View(leerdoel);
        }

        [HttpGet]
        public IActionResult MaakLeerdoel(int onderwijseenheidId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var leerdoel = new Leerdoel();
            leerdoel.OnderwijseenheidId = onderwijseenheidId;

            return View(leerdoel);
        }

        [HttpPost]
        public async Task<IActionResult> MaakLeerdoel(Leerdoel leerdoel)
        {
            SetIdentity();

            if (!ModelState.IsValid)
            {
                AddModelStateErrors();
                return View(leerdoel);
            }

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _leerdoelService.CreateLeerdoel(leerdoel, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(leerdoel);
            }

            return RedirectToAction("OnderwijseenheidDetails", new { onderwijseenheidId = leerdoel.OnderwijseenheidId });
        }

        [HttpGet]
        public async Task<IActionResult> WijzigLeerdoel(int leerdoelId)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var leerdoel = await _leerdoelService.GetLeerdoelById(leerdoelId, jwtToken);

            return View(leerdoel);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigLeerdoel(Leerdoel leerdoel)
        {
            SetIdentity();

            if (!ModelState.IsValid)
            {
                AddModelStateErrors();
                return View(leerdoel);
            }

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _leerdoelService.UpdateLeerdoel(leerdoel.Id, leerdoel, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(leerdoel);
            }

            return RedirectToAction("OnderwijseenheidDetails", new { onderwijseenheidId = leerdoel.OnderwijseenheidId });
        }
    }
}
