using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;

namespace OllieShop.ViewComponents
{
    public class VCAddresses : ViewComponent
    {
        //建立描述資料庫的程式碼，建立後再讀取資料庫的資料表
        private readonly OllieShopContext _context;

        public VCAddresses(OllieShopContext context)
        {
            _context = context;
        }

        //收到參數後撈用戶所有帳戶資料回傳呼叫者
        public async Task<IViewComponentResult> InvokeAsync(long urid)
        {
            var addresses = await _context.Addresses.Where(a => a.URID == urid).ToListAsync();
            return View(addresses);
        }

    }
}
