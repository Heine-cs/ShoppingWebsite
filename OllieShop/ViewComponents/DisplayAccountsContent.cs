using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;

namespace OllieShop.ViewComponents
{
    public class VCAccounts: ViewComponent
    {
        //建立描述資料庫的程式碼，建立後再讀取資料庫的資料表
        private readonly OllieShopContext _context;

        public VCAccounts(OllieShopContext context)
        {
            _context = context;
        }

        //收到參數後撈一筆帳戶資料回傳
        public async Task <IViewComponentResult> InvokeAsync(long acid)
        {
            var accounts = await _context.Accounts.Where(a=>a.ACID == acid).ToListAsync();

            return View(accounts);
        }

    }
}
