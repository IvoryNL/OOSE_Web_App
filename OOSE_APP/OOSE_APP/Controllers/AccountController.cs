using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using OOSE_APP.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HttpResponseException = System.Web.Http.HttpResponseException;

namespace Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            //string filePath = "~/Files/PdfTest.pdf";
            //Response.Headers.Add("Content-Disposition", "inline; filename=test.pdf");
            //return File(filePath, "application/pdf");

            string outputFilePath = "C:\\Users\\ivanf\\Documents\\OOSE Casus\\MaterialGenerator files\\PdfTest.pdf";

            if (!System.IO.File.Exists(outputFilePath))
            {
                return NotFound();
            }

            var fileInfo = new System.IO.FileInfo(outputFilePath);
            Response.ContentType = "application/pdf";
            Response.Headers.Add("Content-Disposition", "attachment;filename=\"" + fileInfo.Name + "\"");
            Response.Headers.Add("Content-Length", fileInfo.Length.ToString());

            // Send the file to the client
            return File(System.IO.File.ReadAllBytes(outputFilePath), "application/pdf", fileInfo.Name);

            var loginModel = new LoginModel(email, password);

            try
            {
                var jwtModel = await _userService.Login(loginModel);
                SetUserIdentity(jwtModel.JwtToken);

                HttpContext.Session.Set("jwtToken", Encoding.UTF8.GetBytes(jwtModel.JwtToken));
            }
            catch (HttpResponseException ex)
            {
                var message = await ex.Response.Content.ReadAsStringAsync();
                var errorViewModel = new ErrorViewModel { StatusCode = ex.Response.StatusCode, ErrorMessage = message };
                return View("Error", errorViewModel);
            }

            return RedirectToAction("Index", "Roles");
        }

        private void SetUserIdentity(string jwtToken)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
            var claimsIdentity = new ClaimsIdentity(token.Claims.ToList(), "<RoleScheme>");
            User.AddIdentity(claimsIdentity);
        }
    }
}
