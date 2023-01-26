using Logic.Constants;
using Logic.Models.Dto;
using Logic.Services.Interfaces;
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

        protected async Task<VolledigeGebruikerModelDto> GetIngelogdeGebruikerByEmail(IGebruikerService gebruikerService, string jwtToken)
        {
            var ingelogdeGebruikerEmail = GetLoggedInUserEmail();
            return await gebruikerService.GetGebruikerByEmail(ingelogdeGebruikerEmail, jwtToken);
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

        protected void AddModelStateErrors(string errorMessage = null)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("all", errorMessage);
                return;
            }

            foreach (var modelstateValue in ModelState.Values)
            {
                foreach (var modelStateError in modelstateValue.Errors)
                {
                    ModelState.AddModelError("all", modelStateError.ErrorMessage);
                }
            }
        }

        protected IActionResult CreateDownloadFile(byte[] fileContent, string contentType, string outputFilename)
        {
            Response.ContentType = contentType;
            Response.Headers.Add("Content-Disposition", "attachment;filename=\"" + outputFilename + "\"");

            return File(fileContent, contentType, outputFilename);
        }

        protected async Task<byte[]> ReadFileContent(IFormFile file)
        {
            var tempFile = $"{Path.GetTempPath()}{file.FileName}";
            using (var stream = System.IO.File.Create(tempFile))
            {
                await file.CopyToAsync(stream);
            }

            return System.IO.File.ReadAllBytes(tempFile);
        }

        private ClaimsIdentity GetClaimsIdentity()
        {
            var jwtToken = Encoding.Default.GetString(HttpContext.Session.Get("jwtToken"));
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
            return new ClaimsIdentity(token.Claims.ToList(), "<RoleScheme>");
        }
    }
}
