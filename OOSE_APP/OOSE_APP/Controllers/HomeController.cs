using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;

using System.Text;

namespace OOSE_APP.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (!IsUerLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
    }
}