using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using Logic.Enums;
using Logic.Models;
using Status = Logic.Enums.Status;

namespace Presentation.Controllers
{
    public class BeoordelingenController : BaseController
    {
        private readonly IGebruikerService _gebruikerService;
        private readonly IKlasService _klasService;
        private readonly IBeoordelingService _beoordelingService;
        private readonly IBeoordelingsmodelService _beoordelingsmodelService;

        public BeoordelingenController(
            IGebruikerService gebruikerService,
            IKlasService klasService,
            IBeoordelingService beoordelingService,
            IBeoordelingsmodelService beoordelingsmodelService)
        {
            _gebruikerService = gebruikerService;
            _klasService = klasService;
            _beoordelingService = beoordelingService;
            _beoordelingsmodelService = beoordelingsmodelService;
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
            var klassen = await _klasService.GetAllKlassen(jwtToken);

            return View(klassen);
        }

        [HttpGet]
        public async Task<IActionResult> BekijkStudenten(int id)
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
            var klas = await _klasService.GetKlasById(id, jwtToken);
            var studenten = klas.Gebruikers.Where(g => g.RolId == (int)RollenEnum.Student).ToList();

            return View(studenten);
        }

        [HttpGet]
        public async Task<IActionResult> BekijkTentamens(int id)
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
            var student = await _gebruikerService.GetGebruikerById(id, jwtToken);
            var tentamens = student.TentamensVanStudent;

            return View(tentamens);
        }

        [HttpGet]
        public async Task<IActionResult> BeoordeelTentamen(int tentamenVanStudentId, int tentamenId)
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
            var beoordelingsmodel = await _beoordelingsmodelService.GetBeoordelingsmodelByTentamenId(tentamenId, jwtToken);
            var docent = await GetIngelogdeGebruikerByEmail(_gebruikerService, jwtToken);
            var beoordeling = new Beoordeling();
            beoordeling.TentamenUploadId = tentamenVanStudentId;
            beoordeling.DocentId = docent.Id;
            beoordeling.Datum = DateTime.Now;
            beoordeling.BeoordelingsmodelId = beoordelingsmodel.Id;

            return View(beoordeling);
        }

        [HttpPost]
        public async Task<IActionResult> BeoordeelTentamen(Beoordeling beoordeling)
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
            await _beoordelingService.CreateBeoordeling(beoordeling, jwtToken);

            return RedirectToAction("Index");
        }
    }
}
