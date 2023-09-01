using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using OllieShop.Models;
using OllieShop.ViewModels;

namespace OllieShop.Controllers
{
    public class UsersFrontEndPageController : Controller
    {
        private readonly OllieShopContext _context;

        public UsersFrontEndPageController(OllieShopContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(long URID)
        {
            if (URID == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.URID == URID);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRegisterViewmodel registerViewmodel)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users();
                user.Name = registerViewmodel.Name;
                user.Gender = registerViewmodel.Gender;
                user.Email = registerViewmodel.Email;
                user.BirthDay = registerViewmodel.BirthDay;
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                //Users資料表新增後，取出identity生成的主鍵，再填到Accounts、Addresses資料表當外鍵用
                

                Accounts account = new Accounts();
                account.Account = registerViewmodel.Account;
                account.Password = registerViewmodel.Password;
                account.Level = registerViewmodel.Level;
                account.URID = user.URID;//填外鍵

                Addresses address = new Addresses();
                address.District = registerViewmodel.District;
                address.Street = registerViewmodel.Street;
                address.City = registerViewmodel.City;
                address.Phone = registerViewmodel.Phone;
                address.URID = user.URID;//填外鍵

                await _context.Accounts.AddAsync(account);
                await _context.Addresses.AddAsync(address);

                await _context.SaveChangesAsync();

                TempData["forUserIdentity"] = user.URID.ToString();//註冊後將用戶ID提取，要給用戶註冊身分頁面使用
                return RedirectToAction("Create", "CustomersFrontEndPage");
                //return Redirect登入action
            }
            ModelState.AddModelError(string.Empty, "檢查輸入資料是否正確?");
            return View(registerViewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WriteEditFormPage(long urid)
        {
            if (urid == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(urid);
            if (users == null)
            {
                return NotFound();
            }
            return View(nameof(Edit),users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("URID,Name,Gender,Email,BirthDay")] Users users)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.URID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["editSuccessMessage"] = "本次修改成功! 您可再次確認是否符合預期";
                return View(nameof(Details),users);
            }
            return View(users);
        }

        private bool UsersExists(long id)
        {
          return (_context.Users?.Any(e => e.URID == id)).GetValueOrDefault();
        }
    }
}
