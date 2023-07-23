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
    public class DisplayAnnouncementsForUserController : Controller
    {
        private readonly OllieShopContext _context;

        public DisplayAnnouncementsForUserController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: DisplayAnnouncementsForUser
        public async Task<IActionResult> Index()
        {
            var ollieShopContext = _context.Announcements.Include(a => a.AD).OrderByDescending(a => a.PublicDate);
            return View(await ollieShopContext.ToListAsync());
        }

        // GET: DisplayAnnouncementsForUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Announcements == null)
            {
                return NotFound();
            }

            var announcements = await _context.Announcements
                .Include(a => a.AD)
                .FirstOrDefaultAsync(m => m.ATID == id);
            if (announcements == null)
            {
                return NotFound();
            }

            return View(announcements);
        }

    }
}
