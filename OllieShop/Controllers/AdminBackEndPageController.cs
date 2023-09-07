using Microsoft.AspNetCore.Mvc;

namespace OllieShop.Controllers
{
    public class AdminBackEndPageController : Controller
    {
        public IActionResult LandingPage()
        {
            return View();
        }
    }
}
