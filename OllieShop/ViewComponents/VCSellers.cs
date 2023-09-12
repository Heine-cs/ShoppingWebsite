using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;

namespace OllieShop.ViewComponents
{
    public class VCSellers:ViewComponent
    {
        private readonly OllieShopContext _context;
        public VCSellers(OllieShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long SRID)
        {
            Sellers sellers = await _context.Sellers
                                        .Include(s => s.SellerShipVias)
                                        .Include(s => s.SellerPaymentMethods)
                                        .FirstOrDefaultAsync(s=>s.SRID == SRID);

            //計算Seller上架商品總數目
            ViewData["sellerProductAmount"] = await _context.Products.Where(p => p.SRID == SRID).CountAsync();
            //計算訂單完成數
            ViewData["orderAccomplishAmount"] = await _context.Orders.Where(o => o.SRID == SRID).CountAsync();
            //列舉商家所有付款方式
            ViewData["sellerPaymentMethods"] = _context.SellerPaymentMethods
                                                .Where(spm => spm.SRID == SRID)
                                                .Join(_context.PaymentMethods,
                                                spm => spm.PMID,
                                                pm => pm.PMID,
                                                (spm, pm) => pm.Name)
                                                .ToList();
            //列舉商家所有運送方式
            ViewData["sellerShipVias"] = _context.SellerShipVias
                                                .Where(ssv => ssv.SRID == SRID)
                                                .Join(_context.ShipVias,
                                                ssv => ssv.SVID,
                                                sv => sv.SVID,
                                                (ssv, sv) => sv.Name)
                                                .ToList();
            //列舉商家所有付款方式
            return View(sellers);
        }
    }
}
