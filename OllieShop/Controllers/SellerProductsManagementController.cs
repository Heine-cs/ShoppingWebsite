using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;
using OllieShop.ViewModels;

namespace OllieShop.Controllers
{
    public class SellerProductsManagementController : Controller
    {
        private readonly OllieShopContext _context;
        private readonly IdentityCheck _identityCheck;
        public SellerProductsManagementController(OllieShopContext context, IdentityCheck identityCheck)
        {
            _context = context;
            _identityCheck = identityCheck;
        }

        public async Task<IActionResult> Index(long SRID)
        {
            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(SRID);
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            var productsTable =  await _context.Products.Include(c => c.CY).Include(s => s.SR).Where(p => p.SRID == SRID).ToListAsync();
            var specificationsTable = await _context.Specifications.ToListAsync();

            List<VMSellerProductsManagement> allProductPlusSpec = new List<VMSellerProductsManagement>();
            VMSellerProductsManagement productPlusSpec;
            foreach (var singalDataLine in productsTable)
            {

                productPlusSpec = new VMSellerProductsManagement()
                {
                    //Product的資料寫入物件成員
                    PTID = singalDataLine.PTID,
                    Name = singalDataLine.Name,
                    DeliveryFee = singalDataLine.DeliveryFee,
                    LaunchDate = singalDataLine.LaunchDate,
                    Hidden = singalDataLine.Hidden,
                    Locked = singalDataLine.Locked,
                    Inquired = singalDataLine.Inquired,
                    Installment = singalDataLine.Installment,
                    Unopened = singalDataLine.Unopened,
                    UnitPrice = singalDataLine.UnitPrice,
                    ShelfQuantity = singalDataLine.ShelfQuantity,
                    SoldQuantity = singalDataLine.SoldQuantity,
                    Description = singalDataLine.Description,
                    CYID = singalDataLine.CYID,
                    SRID = singalDataLine.SRID,
                    //specification的資料寫入物件成員
                    Picture = specificationsTable.FirstOrDefault(s => s.PTID == singalDataLine.PTID).Picture.ToString()
                };
                allProductPlusSpec.Add(productPlusSpec);

            }
            //return View(await ollieShopContext.ToListAsync());
            return View(allProductPlusSpec);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.CY)
                .Include(p => p.SR)
                .FirstOrDefaultAsync(m => m.PTID == id);
            if (products == null)
            {
                return NotFound();
            }

            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(products.SRID.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            return View(products);
        }

        public IActionResult Create()
        {
            string sellerInfomation = HttpContext.Session.GetString("SellerInfomation");
            Sellers sellerFullInfo= Newtonsoft.Json.JsonConvert.DeserializeObject<Sellers>(sellerInfomation);

            //檢查上架商品前，是否至少將商店的運送方式與付款方式都註冊一組
            bool sellerShipViasQuery = _context.SellerShipVias.Any(s => s.SRID == sellerFullInfo.SRID);
            bool SellerPaymentMethodsQuery = _context.SellerPaymentMethods.Any(s => s.SRID == sellerFullInfo.SRID);
            if(sellerShipViasQuery == false && SellerPaymentMethodsQuery == false)
            {
                return RedirectToAction(nameof(SignUpShopPaymentMethodAndShipVia), new {sellerFullInfo.SRID});
            }

            if (sellerShipViasQuery == false)
            {
                TempData["BindShipViaReason"] = "您尚未替商店綁定運送方式，需綁定一組方式才能開始上架商品";
                return RedirectToAction("Create", "SellerShipViasManagement", new { sellerFullInfo.SRID });
            }            

            if (SellerPaymentMethodsQuery == false)
            {
                TempData["BindPaymentMethodReason"] = "您尚未替商店綁定付款方式，需綁定一組方式才能開始上架商品";
                return RedirectToAction("Create", "SellerPaymentMethodsManagement", new { sellerFullInfo.SRID });
            }


            ViewData["LaunchDate"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            ViewData["CategorysSelectList"] = new SelectList(_context.Categorys, "CYID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VMSellerProductsManagement sellerProduct)
        {
            if (ModelState.IsValid)
            {
                Products products = new Products() {
                    Name = sellerProduct.Name,
                    DeliveryFee = sellerProduct.DeliveryFee,
                    LaunchDate = sellerProduct.LaunchDate,
                    Hidden = sellerProduct.Hidden,
                    Locked = sellerProduct.Locked,
                    Inquired= sellerProduct.Inquired,
                    Installment = sellerProduct.Installment,
                    Unopened= sellerProduct.Unopened,
                    UnitPrice = sellerProduct.UnitPrice,
                    ShelfQuantity = sellerProduct.ShelfQuantity,
                    SoldQuantity= sellerProduct.SoldQuantity,
                    Description = sellerProduct.Description,
                    CYID = sellerProduct.CYID,
                    SRID = sellerProduct.SRID,
                };
                await _context.Products.AddAsync(products);
                await _context.SaveChangesAsync();
                //Products資料表新增後，取出identity生成的主鍵，再填到Specifications資料表當外鍵用

                Specifications specifications = new Specifications()
                {
                    SpecName = sellerProduct.SpecName,
                    Picture = sellerProduct.Picture,
                    Weight = sellerProduct.Weight,
                    Size= sellerProduct.Size,
                    LeadDay= sellerProduct.LeadDay,
                    PackageSize= sellerProduct.PackageSize,
                    Freebie = sellerProduct.Freebie,
                    PTID = products.PTID//填外鍵
                };

                await _context.Specifications.AddAsync(specifications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {products.SRID});
            }
            ViewData["CategorysSelectList"] = new SelectList(_context.Categorys, "CYID", "Name", sellerProduct.CYID);
            return View(sellerProduct);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(products.SRID.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }
            ViewData["CategorysSelectList"] = new SelectList(_context.Categorys, "CYID", "Name", products.CYID);
            
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PTID,Name,DeliveryFee,LaunchDate,Hidden,Locked,Inquired,Installment,Unopened,UnitPrice,ShelfQuantity,SoldQuantity,Description,CYID,SRID")] Products products)
        {
            if (id != products.PTID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.PTID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { products.SRID });
            }
            ViewData["CategorysSelectList"] = new SelectList(_context.Categorys, "CYID", "Name", products.CYID);
            return View(products);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.CY)
                .Include(p => p.SR)
                .FirstOrDefaultAsync(m => m.PTID == id);
            if (products == null)
            {
                return NotFound();
            }

            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(products.SRID.GetValueOrDefault());
            if (result is NotFoundResult)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: SellerProductsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'OllieShopContext.Products'  is null.");
            }
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {SRID = products.SRID});
        }

        private bool ProductsExists(long id)
        {
          return (_context.Products?.Any(e => e.PTID == id)).GetValueOrDefault();
        }

        //為商店綁定一組付款方式與運送方式
        public async Task<IActionResult> SignUpShopPaymentMethodAndShipVia(long SRID)
        {
            //驗證使用此action對象之賣家身分是否與session資料相符
            IActionResult result = await _identityCheck.SellerIdentityCheckAsync(SRID);
            if (result is NotFoundResult)
            {
                return NotFound();
            }
            ViewData["PMInfo"] = new SelectList(_context.PaymentMethods, "PMID", "Name");
            ViewData["SVInfo"] = new SelectList(_context.ShipVias, "SVID", "Name");
            ViewData["SRID"] = SRID;
            ViewData["BindReason"] = "您尚未替商店綁定付款方式與運送方式，需個別綁定一組交易方式才能開始上架商品";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUpShopPaymentMethodAndShipVia(SignUpShopPaymentMethodAndShipVia signUpShopPaymentMethodAndShipVia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(signUpShopPaymentMethodAndShipVia.sellerPaymentMethod);
                await _context.SaveChangesAsync();
                _context.Add(signUpShopPaymentMethodAndShipVia.sellerShipVia);
                await _context.SaveChangesAsync();
                //綁定成功，開始新增第一筆商店商品
                return RedirectToAction(nameof(Create));
            }
            return View(signUpShopPaymentMethodAndShipVia);
        }

    }
}
