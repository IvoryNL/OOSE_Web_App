using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class TentamensController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
