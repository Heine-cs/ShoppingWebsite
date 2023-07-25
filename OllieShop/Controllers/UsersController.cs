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
    public class UsersController : Controller
    {
        private readonly OllieShopContext _context;

        public UsersController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'OllieShopContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(u => u.URID == id);

            var userSellerData = from s in _context.Sellers
                                 where s.URID == users.URID
                                 select s
                                 ;
            ViewBag.userSellerData = userSellerData.ToList();
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

    }
}
