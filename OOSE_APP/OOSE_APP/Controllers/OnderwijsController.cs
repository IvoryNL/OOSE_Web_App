using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class OnderwijsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
