using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OllieShop.Models;

namespace OllieShop.Controllers
{
    public class AdminsAccountController : Controller
    {
        private readonly OllieShopContext _context;

        public AdminsAccountController(OllieShopContext context)
        {
            _context = context;
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AdminLogin(Admins admins)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //資料庫查無對應帳號密碼
        //        var result = _context.Admins.FirstOrDefault(a => a.Account == admins.Account && a.Password == admins.Password);
        //        if (result == null)
        //        {
        //            ViewData["ErrorMessage"] = "查無此帳號";
        //            return View(admins);
        //        }

        //        HttpContext.Session.SetString("AdminInfomation", JsonConvert.SerializeObject(result));

        //        return RedirectToAction("", "Home");
        //    }
        //    return View(accounts);
        //}
        //public IActionResult Logout()
        //{
        //    CleanLoginSession();
        //    return RedirectToAction("Index", "Home");
        //}

        //private void CleanLoginSession()
        //{
        //    HttpContext.Session.Remove("UserInfomation");
        //    HttpContext.Session.Remove("CustomerInfomation");
        //    HttpContext.Session.Remove("SellerInfomation");
        //}
        //public IActionResult ReLogin()
        //{
        //    CleanLoginSession();
        //    return RedirectToAction(nameof(UserLogin));
        //}
    }
}
