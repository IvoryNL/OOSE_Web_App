using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class LessenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
