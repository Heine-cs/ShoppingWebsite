using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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


        // GET: UsersFrontEndPage/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.URID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: UsersFrontEndPage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersFrontEndPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("URID,Name,Gender,Email,BirthDay")] UserRegisterViewmodel registerViewmodel)
        {
            Users user = new Users() { 
            
            };
            Accounts account = new Accounts() { };
            Addresses address = new Addresses() { };

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return View();//暫時先這樣 先不跳轉到任何畫面，因為要進行測試
                //return Redirect登入action
            }
            ModelState.AddModelError(string.Empty, "檢查輸入資料是否正確?");
            return View(user);
        }

        // GET: UsersFrontEndPage/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: UsersFrontEndPage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("URID,Name,Gender,Email,BirthDay")] Users users)
        {
            if (id != users.URID)
            {
                return NotFound();
            }

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
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        private bool UsersExists(long id)
        {
          return (_context.Users?.Any(e => e.URID == id)).GetValueOrDefault();
        }
    }
}
