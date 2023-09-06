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
    public class SellerOrdersManagementController : Controller
    {
        private readonly OllieShopContext _context;
        private readonly IdentityCheck _identityCheck;

        public SellerOrdersManagementController(OllieShopContext context, IdentityCheck identityCheck)
        {
            _context = context;
            _identityCheck = identityCheck;
        }

        // GET: SellerOrdersManagement
        public async Task<IActionResult> Index(long SRID)
        {
            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(SRID);
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            var ollieShopContext = await _context.Orders
                .Include(o => o.AS)
                .Include(o => o.CN)
                .Include(o => o.CR)
                .Include(o => o.PC)
                .Include(o => o.PM)
                .Include(o => o.SR)
                .Include(o => o.SV)
                .Where(o => o.SRID == SRID)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            List<string> usersNameByOrder = new List<string>();
            string userName = "";
            foreach (var item in ollieShopContext)
            {
                userName = _context.Users.Where(u => u.URID == item.CR.URID)
                              .Select(u => u.Name).FirstOrDefault().ToString();
                usersNameByOrder.Add(userName);
            }
            ViewData["usersNameByOrder"] = usersNameByOrder;
            return View(ollieShopContext);
        }

        // GET: SellerOrdersManagement/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.AS)
                .Include(o => o.CN)
                .Include(o => o.CR)
                .Include(o => o.PC)
                .Include(o => o.PM)
                .Include(o => o.SR)
                .Include(o => o.SV)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(m => m.ORID == id);
            if (orders == null)
            {
                return NotFound();
            }

            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(orders.SRID.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            Products product = new Products();
            List<Products> purchaseProductsInfo = new List<Products>();
            Specifications Specification = new Specifications();
            List<Specifications> purchaseProductsSpecificationInfo = new List<Specifications>();

            foreach (var item in orders.OrderDetails)
            {
                product = _context.Products.FirstOrDefault(p => p.PTID == item.PTID);
                purchaseProductsInfo.Add(product);
                Specification = _context.Specifications.FirstOrDefault(p => p.SNID == item.SNID);
                purchaseProductsSpecificationInfo.Add(Specification);
            }
            ViewData["purchaseProductsInfo"] = purchaseProductsInfo;
            ViewData["purchaseProductsSpecificationInfo"] = purchaseProductsSpecificationInfo;

            string customerName = _context.Users.Where(u => u.URID == orders.CR.URID)
                  .Select(u => u.Name).FirstOrDefault().ToString();

            ViewData["customerName"] = customerName;
            return View(orders);
        }
        //取消訂單功能
        // GET: SellerOrdersManagement/Edit/5
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null || _context.Orders == null)
        //    {
        //        return NotFound();
        //    }

        //    var orders = await _context.Orders.FindAsync(id);
        //    if (orders == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(orders);
        //}

        // POST: SellerOrdersManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, [Bind("ORID,OrderDate,PaymentDate,ShippedDate,ArrivalDate,Canceled,Locked,SVID,CRID,ASID,PCID,SRID,PMID,CNID")] Orders orders)
        //{
        //    if (id != orders.ORID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(orders);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrdersExists(orders.ORID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ASID"] = new SelectList(_context.Addresses, "ASID", "City", orders.ASID);
        //    ViewData["CNID"] = new SelectList(_context.Coupons, "CNID", "CODE", orders.CNID);
        //    ViewData["CRID"] = new SelectList(_context.Customers, "CRID", "CRID", orders.CRID);
        //    ViewData["PCID"] = new SelectList(_context.PaymentCards, "PCID", "BillAddress", orders.PCID);
        //    ViewData["PMID"] = new SelectList(_context.PaymentMethods, "PMID", "PMID", orders.PMID);
        //    ViewData["SRID"] = new SelectList(_context.Sellers, "SRID", "BankAccount", orders.SRID);
        //    ViewData["SVID"] = new SelectList(_context.ShipVias, "SVID", "SVID", orders.SVID);
        //    return View(orders);
        //}

        //private bool OrdersExists(long id)
        //{
        //  return _context.Orders.Any(e => e.ORID == id);
        //}
    }
}
