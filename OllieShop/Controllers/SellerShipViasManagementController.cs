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
    public class SellerShipViasManagementController : Controller
    {
        private readonly OllieShopContext _context;

        public SellerShipViasManagementController(OllieShopContext context)
        {
            _context = context;
        }

        // GET: SellerShipViasManagement
        public async Task<IActionResult> Index(long SRID)
        {
            if (SRID == 0)
            {
                return NotFound();
            }

            var ollieShopContext = _context.SellerShipVias.Include(s => s.Sellers).Include(s => s.ShipVias).Where(s=>s.SRID == SRID);
            if (ollieShopContext.Count() == 0)
            {
                return RedirectToAction("Create",new { SRID });
            }

            ViewData["SRID"] = SRID;
            return View(await ollieShopContext.ToListAsync());
        }

        // GET: SellerShipViasManagement/Create
        public IActionResult Create(long SRID)
        {
            if (SRID == 0)
            {
                return NotFound();
            }

            ViewData["SRID"] = SRID;
            ViewData["SVInfo"] = new SelectList(_context.ShipVias, "SVID", "Name");
            return View();
        }

        // POST: SellerShipViasManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SRID,SVID,Canceled")] SellerShipVias sellerShipVias)
        {
            //撈出資料庫與sellerPaymentMethods相符的資料，只是為了判斷是否存在，存在就不能進行POST
            var compareQuery = from s in _context.SellerShipVias
                               where ((s.SRID == sellerShipVias.SRID && s.SVID == sellerShipVias.SVID))
                               select s;
            if (!compareQuery.Any()) { 
                if (ModelState.IsValid)
                {
                    _context.Add(sellerShipVias);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index",new {sellerShipVias.SRID});
                }
            }
            ViewData["SVInfo"] = new SelectList(_context.ShipVias, "SVID", "Name", sellerShipVias.SVID);
            ViewData["SRID"] = sellerShipVias.SRID;
            ViewData["ErrorMessage"] = "已經擁有此種運送方式，不能再次新增既存運送方式";
            return View(sellerShipVias);
        }

        // GET: SellerShipViasManagement/Edit/5
        public async Task<IActionResult> Edit(long? SRID,string SVID,string ShipViasName)
        {
            if (SRID == null || SVID == null|| _context.SellerShipVias == null)
            {
                return NotFound();
            }

            var sellerShipVias = await _context.SellerShipVias.FindAsync(SRID,SVID);
            if (sellerShipVias == null)
            {
                return NotFound();
            }
            ViewData["SRID"] = SRID;
            ViewData["SVInfo"] = new SelectList(_context.ShipVias, "SVID", "Name", sellerShipVias.SVID);
            ViewData["ShipViasName"] = ShipViasName;
            return View(sellerShipVias);
        }

        // POST: SellerShipViasManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long SRID,string SVID, [Bind("SRID,SVID,Canceled")] SellerShipVias sellerShipVias)
        {
            if (SRID != sellerShipVias.SRID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sellerShipVias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerShipViasExists(sellerShipVias.SRID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { sellerShipVias.SRID });
            }
            return RedirectToAction("Index", new { sellerShipVias.SRID });
        }

        private bool SellerShipViasExists(long id)
        {
          return _context.SellerShipVias.Any(e => e.SRID == id);
        }
    }
}
