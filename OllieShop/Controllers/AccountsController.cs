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
                //資料庫查無對應帳號密碼
                var result = _context.Accounts.FirstOrDefault(a => a.Account == accounts.Account && a.Password == accounts.Password);
                if (result == null)
                {
                    ViewData["ErrorMessage"] = "查無此帳號";
                    return View(accounts);
                }
                var userInfo = _context.Users.FirstOrDefault(u => u.URID == result.URID);
                if (userInfo != null)
                {
                    HttpContext.Session.SetString("UserInfomation", JsonConvert.SerializeObject(userInfo));
                }
                //判斷登入用戶具有消費者與業者何種身分，並將其具有所有的身分資料儲存至Session
                var customerInfo= _context.Customers.FirstOrDefault(c => c.URID == result.URID);
                if(customerInfo != null)
                {
                    HttpContext.Session.SetString("CustomerInfomation", JsonConvert.SerializeObject(customerInfo));
                }
                
                var sellerInfo= _context.Sellers.FirstOrDefault(s=>s.URID == result.URID);
                if(sellerInfo != null)
                {
                    HttpContext.Session.SetString("SellerInfomation", JsonConvert.SerializeObject(sellerInfo));
                }
				return RedirectToAction("Index", "Home");
			}
			return View(accounts);
		}
        public IActionResult Logout()
        {
            CleanLoginSession();
            return RedirectToAction("Index","Home");
        }

        private void CleanLoginSession()
        {
            HttpContext.Session.Remove("UserInfomation");
            HttpContext.Session.Remove("CustomerInfomation");
            HttpContext.Session.Remove("SellerInfomation");
        }
        public IActionResult ReLogin()
        {
            CleanLoginSession();
            return RedirectToAction(nameof(UserLogin));
        }
    }
}
