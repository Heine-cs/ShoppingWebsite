using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;
using OllieShop.ViewComponents;
using OllieShop.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OllieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OllieShopContext _context;
        public HomeController(ILogger<HomeController> logger, OllieShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productsTable = await _context.Products.Include(c => c.CY).Include(s => s.SR).ToListAsync();
            var specificationsTable = await _context.Specifications.ToListAsync();
            List<VMProductWithSpecification> allProductPlusSpec = new List<VMProductWithSpecification>();
            VMProductWithSpecification productPlusSpec;
            foreach (var singalDataLine in productsTable) {
                //將商品與規格資料combine為單一物件並寫入物件集合中 start
                productPlusSpec = new VMProductWithSpecification()
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
                    Description = singalDataLine.Description == "無描述" ? "": singalDataLine.Description,
                    CYID = singalDataLine.CYID,
                    SRID = singalDataLine.SRID,
                    //specification的資料寫入物件成員
                    Picture = specificationsTable.FirstOrDefault(s => s.PTID == singalDataLine.PTID).Picture.ToString()
                };
                allProductPlusSpec.Add(productPlusSpec);
                //將商品與規格資料combine為單一物件並寫入物件集合中 end
            }
            return View(allProductPlusSpec);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Assistant()
        {
            //協助中心View的Q&A
            Accordion accordion = new Accordion();
            List<string> Question_With_Answer = new List<string> {
            accordion.accordionGenerator("Q: OllieShop是什麼?", "A: OllieShop是一個線上購物平台，提供各種商品和服務，從時尚服飾到家居用品，從電子產品到美妝保健品，滿足您的各種購物需求。", 1),
            accordion.accordionGenerator("Q: 如何在OllieShop上購物?","A: 在OllieShop上購物非常簡單！您只需進入我們的網站，創建一個帳戶，瀏覽我們的產品分類，選擇您喜歡的商品，加入購物車，然後選擇結帳方式完成訂單。",2),
            accordion.accordionGenerator("Q: OllieShop接受哪些支付方式?","A: 我們接受多種支付方式，包括信用卡、貨到付款和其他線上支付平台。您可以在結帳時選擇您喜歡的支付方式",3),
            accordion.accordionGenerator("Q: OllieShop的退換貨政策如何?","A: 我們提供退換貨服務，以確保您對購買的商品滿意。如果您對商品有任何問題或不滿意，請在收到商品後的一周內與我們的客戶服務團隊聯繫。我們將指導您完成退換貨程序並提供必要的協助。",4),
            accordion.accordionGenerator("Q: OllieShop的送貨方式和時間是多久?","A: 商家提供多種送貨方式，包括標準宅配和超商取貨。送貨時間取決於您所在的地區以及您選擇的送貨方式。通常情況下需要3至7個工作日。",5),
            accordion.accordionGenerator("Q: 我可以在OllieShop上追蹤我的訂單嗎?","A: 是的，您可以在OllieShop上追蹤您的訂單。一旦您完成訂單，我們將向您提供一個訂單追蹤號碼，您可以使用此號碼在我們的網站上追蹤您的訂單狀態。",6),
            accordion.accordionGenerator("Q: 我可以取消或修改我的訂單嗎?","A: 是的，如果您希望取消或修改訂單，請在收到訂單確認郵件後盡快與我們的客戶服務團隊聯繫。我們將盡力滿足您的需求，但請注意，如果訂單已經處於發貨或運送階段，可能無法進行取消或修改。",7),
            accordion.accordionGenerator("Q: 我可以在OllieShop上設立自己的賣場嗎?","A: 是的，我們歡迎商家在OllieShop上設立自己的賣場。如果您是一家企業或個人商家，有興趣在我們的平台上銷售商品，請與我們的商家合作團隊聯繫，我們將提供相關的合作資訊和支持。",8)
            };
            ViewData["Question_With_Answer"] = Question_With_Answer;
            //協助中心View的Q&A
            return View();
        }

        public IActionResult OfficialContactInfo()
        {
            return View();
        }

        public async Task<IActionResult> ProductPage(long PTID)
        {
            Products Product= _context.Products.FirstOrDefault(p => p.PTID == PTID);
            List<Specifications> Specifications= await _context.Specifications.Where(p => p.PTID == PTID).ToListAsync();
            List<VMProductWithSpecification> ProductPlusSpecificationCollection = new List<VMProductWithSpecification>();
            VMProductWithSpecification singleProductWholeData;
            foreach(var singalDataLine in Specifications)
            {
                singleProductWholeData = new VMProductWithSpecification()
                {
                    //Product的資料寫入物件成員
                    PTID = Product.PTID,
                    Name = Product.Name,
                    DeliveryFee = Product.DeliveryFee,
                    LaunchDate = Product.LaunchDate,
                    Hidden = Product.Hidden,
                    Locked = Product.Locked,
                    Inquired = Product.Inquired,
                    Installment = Product.Installment,
                    Unopened = Product.Unopened,
                    UnitPrice = Product.UnitPrice,
                    ShelfQuantity = Product.ShelfQuantity,
                    SoldQuantity = Product.SoldQuantity,
                    Description = Product.Description,
                    CYID = Product.CYID,
                    SRID = Product.SRID,
                    //specification的資料寫入物件成員
                    SNID = singalDataLine.SNID,
                    SpecName = singalDataLine.SpecName,
                    Picture = singalDataLine.Picture.ToString(),
                    Weight = singalDataLine.Weight,
                    Size = singalDataLine.Size,
                    LeadDay = singalDataLine.LeadDay,
                    PackageSize = singalDataLine.PackageSize,
                    Freebie = singalDataLine.Freebie,

                };
                ProductPlusSpecificationCollection.Add(singleProductWholeData);

            }
            return View(ProductPlusSpecificationCollection);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}