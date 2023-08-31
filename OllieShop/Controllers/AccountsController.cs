using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            if (ModelState.IsValid)
            {
                var result = _context.Accounts.FirstOrDefault(a => a.Account == accounts.Account && a.Password == accounts.Password);
                if (result == null)
                {
                    ViewData["ErrorMessage"] = "帳號或密碼錯誤";
                    return View(accounts);
                }
				HttpContext.Session.SetString("User", JsonConvert.SerializeObject(result));
				return RedirectToAction("Index", "Home");
			}
			return View(accounts);
		}
    }
}
