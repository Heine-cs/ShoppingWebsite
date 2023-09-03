using Microsoft.AspNetCore.Mvc;

namespace OllieShop.Controllers
{
    public class CustomersBackEndPageController : Controller
    {
        public IActionResult OptionsMenu()
        {
            return View();
        }
    }
}
