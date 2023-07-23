using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;

namespace OllieShop.Controllers
{
    public class CustomersController : Controller
    {
        private readonly OllieShopContext _context;

        public CustomersController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            TempData["forUserIdentity"] = TempData["forUserIdentity"];//接受來自用戶註冊action，裡面放該用戶的URID
                                                                      //因為TempData生命週期只能跨越控制器一次，User註冊後產生的TempData陽壽燒完了，這裡續命接關
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CRID,URID")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));//暫時先這樣，要回到登入頁面，但還沒製作
            }
            
            return View(customers);
        }

        private bool CustomersExists(long id)
        {
          return (_context.Customers?.Any(e => e.CRID == id)).GetValueOrDefault();
        }
    }
}
