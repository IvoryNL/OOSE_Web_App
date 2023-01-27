using Logic.Constants;
using Logic.DocumentExporter.Onderwijseenheden;
using Logic.DocumentExporter.Onderwijsmodules;
using Logic.DocumentImporter.Onderwijseenheden;
using Logic.DocumentImporter.Onderwijsmodules;
using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly ILeeruitkomstService _leeruitkomstService;
        private readonly IOpleidingService _opleidingService;
        private readonly IConsistentieCheckService _consistentieCheckService;

        public OnderwijsController(
            IOnderwijsmoduleService onderwijsmoduleService,
            IOnderwijseenheidService onderwijseenheidService,
            ILeerdoelService leerdoelService,
            ILeeruitkomstService leeruitkomstService,
            IOpleidingService opleidingService,
            IConsistentieCheckService consistentieCheckService)
        {
            _onderwijsmoduleService = onderwijsmoduleService;
            _onderwijseenheidService = onderwijseenheidService;
            _leerdoelService = leerdoelService;
            _leeruitkomstService = leeruitkomstService;
            _opleidingService = opleidingService;
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

            if (!IsWerknemer())
            {
                return Unauthorized();
            }

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

            if (!IsWerknemer())
            {
                return Unauthorized();
            }

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var isConsistent = await _consistentieCheckService.ConsistentieCheckCoverage(onderwijsmoduleId, jwtToken);
            if (!isConsistent)
            {
                ViewData["Consistentie"] = "Deze onderwijsmodule bevat leeruitkomsten zonder tentamen of les.";
            }

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

            if (!IsWerknemer())
            {
                return Unauthorized();
            }

            var onderwijsModule = new Onderwijsmodule();
            onderwijsModule.OpleidingId = opleidingid;

            return View(onderwijsModule);
        }

        [HttpPost]
        public async Task<IActionResult> MaakOnderwijsmodule(Onderwijsmodule onderwijsmodule)
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

            if (!IsWerknemer())
            {
                return Unauthorized();
            }

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var onderwijsModule = await _onderwijsmoduleService.GetOnderwijsmoduleById(onderwijsmoduleId, jwtToken);

            return View(onderwijsModule);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigOnderwijsmodule(Onderwijsmodule onderwijsmodule)
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
        public async Task<IActionResult> BestaandeOnderwijseenheidToevoegen(int onderwijsmoduleId)
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
            var viewModel = new OnderwijsmoduleBestaandeOnderwijseenhedenViewModel();
            viewModel.Onderwijsmodule = await _onderwijsmoduleService.GetOnderwijsmoduleById(onderwijsmoduleId, jwtToken);
            viewModel.BestaandeOnderwijseenheden = await _onderwijseenheidService.GetAllOnderwijseenheden(jwtToken);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> BestaandeOnderwijseenheidToevoegen(OnderwijsmoduleBestaandeOnderwijseenhedenViewModel viewModel)
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
            var onderwijseenheid = await _onderwijseenheidService.GetOnderwijseenheidById(int.Parse(viewModel.GeselecteerdeOnderwijseenheidId), jwtToken);
            await _onderwijsmoduleService.VoegOnderwijseenheidToe(viewModel.Onderwijsmodule.Id, onderwijseenheid, jwtToken);

            return RedirectToAction("OnderwijsmoduleDetails", new { onderwijsmoduleId = viewModel.Onderwijsmodule.Id });
        }

        [HttpGet]
        public async Task<IActionResult> OnderwijseenheidDetails(int onderwijseenheidId)
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

            if (!IsWerknemer())
            {
                return Unauthorized();
            }

            var viewModel = new OnderwijseenheidViewModel();
            viewModel.OnderwijsmoduleId = onderwijsmoduleId;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MaakOnderwijseenheid(OnderwijseenheidViewModel onderwijseenheidViewModel)
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

            return RedirectToAction("OnderwijsmoduleDetails", new { onderwijsmoduleId = onderwijseenheidViewModel.OnderwijsmoduleId });
        }

        [HttpGet]
        public async Task<IActionResult> WijzigOnderwijseenheid(int onderwijseenheidId, int onderwijsmoduleId)
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
            var viewModel = new OnderwijseenheidViewModel();
            viewModel.OnderwijsmoduleId = onderwijsmoduleId;
            viewModel.Onderwijseenheid =  await _onderwijseenheidService.GetOnderwijseenheidById(onderwijseenheidId, jwtToken);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigOnderwijseenheid(OnderwijseenheidViewModel onderwijseenheidViewModel)
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

            return RedirectToAction("OnderwijsmoduleDetails", new { onderwijsmoduleId = onderwijseenheidViewModel.OnderwijsmoduleId });
        }

        [HttpGet]
        public async Task<IActionResult> LeerdoelDetails(int leerdoelId)
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

            if (!IsWerknemer())
            {
                return Unauthorized();
            }

            var leerdoel = new Leerdoel();
            leerdoel.OnderwijseenheidId = onderwijseenheidId;

            return View(leerdoel);
        }

        [HttpPost]
        public async Task<IActionResult> MaakLeerdoel(Leerdoel leerdoel)
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

            if (!IsWerknemer())
            {
                return Unauthorized();
            }

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
            var leerdoel = await _leerdoelService.GetLeerdoelById(leerdoelId, jwtToken);

            return View(leerdoel);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigLeerdoel(Leerdoel leerdoel)
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

        [HttpGet]
        public async Task<IActionResult> LeeruitkomstDetails(int leeruitkomstId)
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
            var leerdoel = await _leeruitkomstService.GetLeeruitkomstById(leeruitkomstId, jwtToken);

            return View(leerdoel);
        }

        [HttpGet]
        public IActionResult MaakLeeruitkomst(int leerdoelId)
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

            var leeruitkomst = new Leeruitkomst();
            leeruitkomst.LeerdoelId = leerdoelId;

            return View(leeruitkomst);
        }

        [HttpPost]
        public async Task<IActionResult> MaakLeeruitkomst(Leeruitkomst leeruitkomst)
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

            if (!ModelState.IsValid)
            {
                AddModelStateErrors();
                return View(leeruitkomst);
            }

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _leeruitkomstService.Createleeruitkomst(leeruitkomst, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(leeruitkomst);
            }

            return RedirectToAction("LeerdoelDetails", new { leerdoelId = leeruitkomst.LeerdoelId });
        }

        [HttpGet]
        public async Task<IActionResult> WijzigLeeruitkomst(int leeruitkomstId)
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
            var leeruitkomst = await _leeruitkomstService.GetLeeruitkomstById(leeruitkomstId, jwtToken);

            return View(leeruitkomst);
        }

        [HttpPost]
        public async Task<IActionResult> WijzigLeeruitkomst(Leeruitkomst leeruitkomst)
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

            if (!ModelState.IsValid)
            {
                AddModelStateErrors();
                return View(leeruitkomst);
            }

            try
            {
                var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);
                await _leeruitkomstService.UpdateLeeruitkomst(leeruitkomst.Id, leeruitkomst, jwtToken);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                ModelState.AddModelError("all", message);
                return View(leeruitkomst);
            }

            return RedirectToAction("LeerdoelDetails", new { leerdoelId = leeruitkomst.LeerdoelId });
        }

        [HttpGet]
        public IActionResult ImporteerOnderwijsmodule(int opleidingId)
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

            var viewModel = new ImporteerOnderwijsmoduleViewModel();
            viewModel.OpleidingId = opleidingId;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ImporteerOnderwijsmodule(ImporteerOnderwijsmoduleViewModel viewModel)
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

            var onderwijsmodule = new Logic.Models.DocumentExportEnImport.Onderwijsmodule();
            onderwijsmodule.ImporteerDocument = new ImportOnderwijsmoduleFromJsonStringStrategy();
            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);

            try
            {
                onderwijsmodule = onderwijsmodule.ImporteerDocument.ImportDocument(await ReadFileContent(viewModel.File));
                await _onderwijsmoduleService.ImporteerOnderwijsmodule(onderwijsmodule, jwtToken);
            }
            catch (JsonSerializationException jsonException)
            {
                var message = "Het bestand bevat geen valide data.";
                AddModelStateErrors(jsonException.Message);
                return View(viewModel);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                AddModelStateErrors(message);
                return View(viewModel);
            }

            return RedirectToAction("OverzichtOnderwijsmodules", new { opleidingId = viewModel.OpleidingId });
        }

        [HttpGet]
        public async Task<IActionResult> ExporteerOnderwijsmodule(int onderwijsmoduleId, string bestandsformaat)
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
            var onderwijsmodule = await _onderwijsmoduleService.GetOnderwijsmoduleVoorExportById(onderwijsmoduleId, jwtToken);
            onderwijsmodule.ExporteerDocument = new ExportOnderwijsmoduleToJsonStrategy();

            var onderwijsmoduleConentBytes = onderwijsmodule.ExporteerDocument.ExportToDocument(onderwijsmodule);
            var outputBestand = $"{onderwijsmodule.Naam}{bestandsformaat}";

            return CreateDownloadFile(onderwijsmoduleConentBytes, ContentTypes.JSON, outputBestand);
        }

        [HttpGet]
        public IActionResult ImporteerOnderwijseenheid(int onderwijsmoduleId)
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

            var viewModel = new ImporteerOnderwijseenheidViewModel();
            viewModel.OnderwijsmoduleId = onderwijsmoduleId;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ImporteerOnderwijseenheid(ImporteerOnderwijseenheidViewModel viewModel)
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

            var onderwijseenheid = new Logic.Models.DocumentExportEnImport.Onderwijseenheid();
            onderwijseenheid.ImporteerDocument = new ImportOnderwijseenheidFromJsonStringStrategy();
            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);

            try
            {
                onderwijseenheid = onderwijseenheid.ImporteerDocument.ImportDocument(await ReadFileContent(viewModel.File));
                await _onderwijsmoduleService.ImporteerOnderwijseenheid(viewModel.OnderwijsmoduleId, onderwijseenheid, jwtToken);
            }            
            catch (JsonSerializationException jsonException)
            {
                var message = "Het bestand bevat geen valide data.";
                AddModelStateErrors(jsonException.Message);
                return View(viewModel);
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                AddModelStateErrors(message);
                return View(viewModel);
            }

            return RedirectToAction("OnderwijsmoduleDetails", new { onderwijsmoduleId = viewModel.OnderwijsmoduleId });
        }

        [HttpGet]
        public async Task<IActionResult> ExporteerOnderwijseenheid(int onderwijseenheidId, string bestandsformaat)
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
            var onderwijseenheid = await _onderwijseenheidService.GetOnderwijseenheidVoorExportById(onderwijseenheidId, jwtToken);
            onderwijseenheid.ExporteerDocument = new ExportOnderwijseenheidToJsonStrategy();

            var onderwijsmoduleConentBytes = onderwijseenheid.ExporteerDocument.ExportToDocument(onderwijseenheid);
            var outputBestand = $"{onderwijseenheid.Naam}{bestandsformaat}";

            return CreateDownloadFile(onderwijsmoduleConentBytes, ContentTypes.JSON, outputBestand);
        }
    }
}
