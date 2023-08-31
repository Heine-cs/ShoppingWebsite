using Microsoft.AspNetCore.Mvc;
using OllieShop.Models;

namespace OllieShop.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserLogin(Accounts accounts)
        {
            return View();
        }
    }
}
