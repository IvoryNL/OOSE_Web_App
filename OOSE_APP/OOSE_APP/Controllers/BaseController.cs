using Logic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    public abstract class BaseController : Controller
    {
        protected bool IsUerLoggedIn()
        {           
            return HttpContext.Session.Keys.Contains("jwtToken");
        }

        protected bool IsUserInRole(string roleName)
        {
            var roleClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
            return roleClaim?.Value == roleName;
        }
    }
}
