using Logic.Models;
using Logic.Models.Constants;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;

namespace Presentation.Controllers
{
    public class RollenController : BaseController
    {
        private readonly IRolesService _rolesService;

        public RollenController(IRolesService rolesService) 
        {
            _rolesService = rolesService;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsUerLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            if (!IsUserInRole(Rollen.DOCENT) || !IsUserInRole(Rollen.ADMIN))
            {
                return Unauthorized();
            }

            var jwtToken = JwtTokenHelper.GetJwtTokenFromSession(HttpContext);            
            var roles = Enumerable.Empty<Rol>();

            try
            {
                roles = await _rolesService.GetAll(jwtToken);
            }
            catch (Exception ex) 
            {
                return View("Error");
            }

            return View(roles);
        }
    }
}
