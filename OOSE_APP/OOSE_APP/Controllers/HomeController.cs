using Logic.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;

namespace OOSE_APP.Controllers
{
    public class HomeController : BaseController
    {

        public IActionResult Index()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Account");
            }

            SetIdentity();

            return View("Index");
        }
    }
}