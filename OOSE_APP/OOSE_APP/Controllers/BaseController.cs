using Logic.Models.Constants;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    public abstract class BaseController : Controller
    {
        protected bool IsUserLoggedIn()
        {           
            return HttpContext.Session.Keys.Contains("jwtToken");
        }

        protected string GetUserRol()
        {
            var claimsIdentity = GetClaimsIdentity();
            var roleClaim = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
            return roleClaim?.Value;
        }

        protected string GetLoggedInUserEmail()
        {
            var claimsIdentity = GetClaimsIdentity();
            var roleClaim = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            return roleClaim?.Value;
        }

        protected void SetIdentity()
        {
            var claimsIdentity = GetClaimsIdentity();
            User.AddIdentity(claimsIdentity);
        }

        protected bool IsWerknemer()
        {
            return !User.IsInRole(Rollen.DOCENT) || !User.IsInRole(Rollen.ADMIN);
        }

        private ClaimsIdentity GetClaimsIdentity()
        {
            var jwtToken = Encoding.Default.GetString(HttpContext.Session.Get("jwtToken"));
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
            return new ClaimsIdentity(token.Claims.ToList(), "<RoleScheme>");
        }
    }
}
