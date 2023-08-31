using Microsoft.AspNetCore.Mvc;
using OllieShop.Models;

namespace OllieShop.Controllers
{
    public class AccountsController : Controller
    {
        private readonly OllieShopContext _context;

        public AccountsController(OllieShopContext context)
        {
            _context = context;
        }

        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserLogin(Accounts accounts)
        {
            if(accounts == null)
            {
                return View();
            }

            return View();
        }
    }
}
