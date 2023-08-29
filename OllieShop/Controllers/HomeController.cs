using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllieShop.Models;
using OllieShop.ViewComponents;
using OllieShop.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Dynamic;
using System.Drawing;
using System.Net.Sockets;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Buffers.Text;
using static NuGet.Packaging.PackagingConstants;
using NuGet.Packaging;
using System.Security.Policy;

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
                //如果singalDataLine.ShelfQuantity == singalDataLine.SoldQuantity，跳過此筆
                if(singalDataLine.ShelfQuantity == singalDataLine.SoldQuantity)
                {
                    continue;
                }
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
                    Description = singalDataLine.Description == "無描述" ? "" : singalDataLine.Description,
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
            Products Product = await _context.Products.FirstOrDefaultAsync(p => p.PTID == PTID);
            List<Specifications> Specifications = await _context.Specifications.Where(p => p.PTID == PTID).ToListAsync();
            List<VMProductWithSpecification> ProductPlusSpecificationCollection = new List<VMProductWithSpecification>();
            VMProductWithSpecification singleProductWholeData;
            foreach (var singalDataLine in Specifications)
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

        //顯示購物車畫面用的function
        [HttpPost]
        public string getCartInfo([FromBody] IEnumerable<IEnumerable<long>> cartData)
        {
            //cartData本來是一個2D陣列，經過下方程式碼轉換就會將2D陣列內所有1D陣列組合成一個集合
            //例如:[[1,2,3][4,5,6][66,50,2]]->[1,2,3,4,5,6,66,50,2]
            IEnumerable<long> cart2DArrayTo1D = cartData.SelectMany(innerCollection => innerCollection);

            //透過迴圈打包成VMProductWithSpecification物件，另外說明:購物車2D陣列的封存邏輯放在_Layout.cshtml檔案中
            long productID = 0;
            long specificationsID = 0;
            int requireQuantities = 0;
            VMProductWithSpecification SingleProdctRequiredInfo = new VMProductWithSpecification();
            List<VMProductWithSpecification> RequireAllProductInfo = new List<VMProductWithSpecification>();
            //先計算for迴圈運轉次數，等於或不等於1會是迴圈索引運作的關鍵，因為當商品數量增加時第一個商品對應索引值將會變動
            var ForloopTimes = (cart2DArrayTo1D.Count()) / 3;
            //計算商品數量增加時第一個商品對應的3個索引值(productID,specificationsID,requireQuantities)
            int f = 0;
            int g = 0;
            for (int h = 1;h< ForloopTimes; h++)
            {
                f++;
                g+=2;
            }
            //迴圈建構商品物件
            for (int i = 0; i < ForloopTimes; i++)
                {
                    productID = cart2DArrayTo1D.ElementAt(i);
                    specificationsID = cart2DArrayTo1D.ElementAt(f+1);
                    //資料表(OrderDetail)訂購數量欄位資料型態定義為INT，但是javascript物件會轉換為cartData物件，
                    //cartData嵌套陣列被設定為LONG資料型態，所以要轉換資料型態requireQuantities避免問題
                    requireQuantities = Convert.ToInt32(cart2DArrayTo1D.ElementAt(g+2));
                    f++;
                    g++;
                    //MakeAndCollectCartProductsObject鑄造購物車單一商品物件後加至List保存，為避免迴圈重複宣告將使用後的SingleProdctRequiredInfo作為參數傳遞
                    RequireAllProductInfo.Add(MakeAndCollectCartProductsObject(productID, specificationsID, requireQuantities,SingleProdctRequiredInfo));
                }

            //將List序列化回傳到layout的ajax接收，接收後才能顯示在想要的地方
            string RequireAllProductInfoJsonType = JsonConvert.SerializeObject(RequireAllProductInfo);
            return RequireAllProductInfoJsonType;
        }

        private VMProductWithSpecification MakeAndCollectCartProductsObject(long productID, long specificationsID, int requireQuantities, VMProductWithSpecification SingleProdctRequiredInfo)
        {
            //建構單一商品訂購物件，先尋得需要商品的相關資料行
            Products Product = _context.Products.FirstOrDefault(p => p.PTID == productID);
            Specifications Specification = _context.Specifications.FirstOrDefault(p => p.SNID == specificationsID);

            //寫入物件成員
            SingleProdctRequiredInfo = new VMProductWithSpecification()
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
                SNID = Specification.SNID,
                SpecName = Specification.SpecName,
                Picture = Specification.Picture.ToString(),
                Weight = Specification.Weight,
                Size = Specification.Size,
                LeadDay = Specification.LeadDay,
                PackageSize = Specification.PackageSize,
                Freebie = Specification.Freebie,
                //需求數量寫入物件成員
                RequireQuantities = requireQuantities
            };
            return SingleProdctRequiredInfo;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string CartTableWithChildNode,long CRID,long URID,int ProductFeeCount)
        {
            //沒有CRID與URID就:導引註冊->登入->結帳(還沒寫)
            ViewData["CartTableWithChildNode"] = CartTableWithChildNode;
            ViewData["OrderEstablishDate"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            //先寫死
            ViewData["CRID"] = 1;
            //URID與CRID寫死
            ViewData["CustomerAddresses"] = new SelectList(_context.Addresses.Where(A => A.URID == 3), "ASID", "Street");
            //取出消費者折價券
            var CustomerUseableCouponsQuery =
                from cc in _context.CustomerCoupons
                join c in _context.Coupons on cc.CNID equals c.CNID
                where cc.CRID == 1 && cc.AppliedDate == null && c.ExpiryDate > DateTime.Now
                select new
                {
                    CNID = cc.CNID,
                    CODE = c.CODE
                };
            ViewData["CustomerUseableCoupons"] = new SelectList(await CustomerUseableCouponsQuery.ToListAsync(), "CNID", "CODE");
            
            var CustomerCouponCODE = CustomerUseableCouponsQuery.Select(item => item.CODE).ToList();

            //Contains對應SQL where In的語法，能將多個條件一次性透過where找出每個條件對應的值
            IQueryable<double> CustomerCouponDiscount = from c in _context.Coupons
                                                        where CustomerCouponCODE.Contains(c.CODE)
                                                        select c.Discount;

            //選中CustomerUseableCoupons的Select option中第幾個元素，就在CustomerCouponDiscount List透過索引值取到折扣比率
            ViewData["CustomerCouponDiscount"] = await CustomerCouponDiscount.ToListAsync();
            ViewData["ProductFeeCount"] = ProductFeeCount;//在View頁面要與CustomerCouponDiscount互相運算

            return View();
        }

        [HttpPost]
        public IActionResult GenerateOrdersBaseOnDifferentProducts(Orders orders,string cartData)
        {
            //接收到的Json格式二位陣列轉成IEnumerable<IEnumerable<long>>資料型態
            IEnumerable<IEnumerable<long>> cartDataOnLong2DArrayType = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<long>>>(cartData);

            //cartData本來是一個2D陣列，經過下方程式碼轉換就會將2D陣列內所有1D陣列組合成一個集合
            //例如:[[1,2,3][4,5,6][66,50,2]]->[1,2,3,4,5,6,66,50,2]
            IEnumerable<long> cart2DArrayTo1D = cartDataOnLong2DArrayType.SelectMany(innerCollection => innerCollection);

            //透過迴圈打包成VMProductWithSpecification物件，另外說明:購物車2D陣列的封存邏輯放在_Layout.cshtml檔案中
            long productID = 0;
            long specificationsID = 0;
            int requireQuantities = 0;
            //new 空殼能避免在迴圈內進入MakeBillItem action時不斷宣告
            VMGenerateOrdersByCartData billItemShell = new VMGenerateOrdersByCartData();
            Products Product = new Products();
            Specifications Specification = new Specifications();
            List<VMGenerateOrdersByCartData> RequireAllProductInfoCombineorders = new List<VMGenerateOrdersByCartData>();
            //先計算for迴圈運轉次數，等於或不等於1會是迴圈索引運作的關鍵，因為當商品數量增加時第一個商品對應索引值將會變動
            var ForloopTimes = (cart2DArrayTo1D.Count()) / 3;
            //計算商品數量增加時第一個商品對應的3個索引值(productID,specificationsID,requireQuantities)
            int f = 0;
            int g = 0;
            for (int h = 1; h < ForloopTimes; h++)
            {
                f++;
                g += 2;
            }
            //迴圈建構商品物件與對應的下拉式選單
            IEnumerable<SelectListItem> sellerPaymentMethods;
            IEnumerable<SelectListItem> customerPaymentCards;
            IEnumerable<SelectListItem> sellerShipVias;
            bool PayByCreditCardMethodExist;
            bool CustomerPaymentCardExist;
            for (int i = 0; i < ForloopTimes; i++)
            {
                productID = cart2DArrayTo1D.ElementAt(i);

                specificationsID = cart2DArrayTo1D.ElementAt(f + 1);
                //資料表(OrderDetail)訂購數量欄位資料型態定義為INT，但是javascript物件會轉換為cartData物件，
                //cartData嵌套陣列被設定為LONG資料型態，所以要轉換資料型態requireQuantities避免問題
                requireQuantities = Convert.ToInt32(cart2DArrayTo1D.ElementAt(g + 2));
                f++;
                g++;
                //MakeAndCollectCartProductsObject鑄造購物車單一商品物件後加至List保存，為避免迴圈重複宣告將Product、Specification、billItemShell作為參數傳遞
                //迴圈帶入Order物件，Order物件中有來自PlaceOrder頁面被消費者填入的屬性值(需要保留的有:地址編號、折價券編號、消費者編號)，
                //MakeBillItem會寫入【orders值、商品資料與數量】至VMGenerateOrdersByCartData物件
                billItemShell = MakeBillItem(productID, specificationsID, requireQuantities, orders, Product, Specification, billItemShell);
                //生成下拉式選單
                //找出Seller詳細的付款方式，如果消費者沒有註冊信用卡須排除select出刷卡的付款方式
                CustomerPaymentCardExist = _context.PaymentCards.Any(item => item.CRID == orders.CRID);
                if(CustomerPaymentCardExist != false) { 
                    sellerPaymentMethods = from sp in _context.SellerPaymentMethods
                                               join pm in _context.PaymentMethods on sp.PMID equals pm.PMID
                                               where sp.SRID == billItemShell.products.SRID && sp.Canceled != true
                                               select new SelectListItem
                                               {
                                                   Value = sp.PMID,
                                                   Text = pm.Name
                                               };
                }
                else
                {
                    sellerPaymentMethods = from sp in _context.SellerPaymentMethods
                                           join pm in _context.PaymentMethods on sp.PMID equals pm.PMID
                                           where sp.SRID == billItemShell.products.SRID && sp.Canceled != true && sp.PMID != "PM02"
                                           select new SelectListItem
                                           {
                                               Value = sp.PMID,
                                               Text = pm.Name
                                           };
                }

                //找出Customer的信用卡，如果賣家存在刷卡的付款方式才去撈卡號，不存在就將卡號選單設為空值
                PayByCreditCardMethodExist = sellerPaymentMethods.Any(item => item.Text == "刷卡");
                if (PayByCreditCardMethodExist != false)
                {
                    customerPaymentCards = from pc in _context.PaymentCards
                                           where pc.CRID == billItemShell.orders.CRID
                                           select new SelectListItem
                                           {
                                               Value = pc.PCID.ToString(),
                                               Text = pc.Number
                                           };
                }
                else
                {
                    customerPaymentCards = Enumerable.Empty<SelectListItem>();
                }
                //找出Seller的運送方式
                sellerShipVias = from ssv in _context.SellerShipVias
                                     join sv in _context.ShipVias on ssv.SVID equals sv.SVID
                                     where ssv.SRID == billItemShell.products.SRID && ssv.Canceled != true
                                     select new SelectListItem
                                     {
                                         Value = ssv.SVID,
                                         Text = sv.Name
                                     };


                //放到viewdata內給View asp-items做為選項使用
                ViewData[$"sellerPaymentMethods{i}"] = sellerPaymentMethods;
                ViewData[$"customerPaymentCards{i}"] = customerPaymentCards;
                ViewData[$"sellerShipVias{i}"] = sellerShipVias;

                RequireAllProductInfoCombineorders.Add(billItemShell);          
            }
            return View(RequireAllProductInfoCombineorders);
        }
        //配合GenerateOrdersBaseOnDifferentProducts action使用的功能
        private VMGenerateOrdersByCartData MakeBillItem(long productID, long specificationsID, int requireQuantities,Orders orders,Products Product, Specifications Specification, VMGenerateOrdersByCartData billItemShell)
        {
            //建構單一商品訂購物件，先尋得需要商品的相關資料行
            Product = _context.Products.FirstOrDefault(p => p.PTID == productID);
            Specification = _context.Specifications.FirstOrDefault(p => p.SNID == specificationsID);

            //寫入物件成員
            billItemShell = new VMGenerateOrdersByCartData()
            {
                products = new Products()
                {
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
                    SRID = Product.SRID
                },
                specifications = new Specifications()
                {
                    SNID = Specification.SNID,
                    SpecName = Specification.SpecName,
                    Picture = Specification.Picture.ToString(),
                    Weight = Specification.Weight,
                    Size = Specification.Size,
                    LeadDay = Specification.LeadDay,
                    PackageSize = Specification.PackageSize,
                    Freebie = Specification.Freebie
                },
                orders = new Orders
                {
                    OrderDate = orders.OrderDate,
                    CRID = orders.CRID,
                    ASID = orders.ASID,
                    CNID = orders.CNID
                    //PCID、SVID、PMID透過下拉式選單帶值
                },
                RequireQuantities = requireQuantities
            };
            return billItemShell;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> processOrders(List<VMReceiveUnhandleOrders> ordersRequireMaterial)
        {
            Orders orders = null;
            OrderDetails orderDetails = null;
            Products productDataLineGetFromTable = null;
            CustomerCoupons customerCouponDataLineGetFromTable = null;
            foreach (VMReceiveUnhandleOrders item in ordersRequireMaterial)
            {
                //如果發現找到沒有填必填欄位的物件(付款方式、信用卡卡號、運送方式三者之一)，這代表這張訂單有著同一商家不同商品的情境，
                //此種情形不需新增Orders資料表資料行，新增時僅需新增到OrderDetail資料表
                if (item.sellerShipViaOptions == null)
                {             
                    //找出同樣SRID且sellerShipViaOptions值不為空的物件，物件中有一個屬性需要提取出來(ORID)，塞到當前迴圈處理的物件當中
                    foreach(var allOrderFieldsCompleteProductItem in ordersRequireMaterial)
                    {
                        if(item.products.SRID == allOrderFieldsCompleteProductItem.products.SRID && allOrderFieldsCompleteProductItem.orders.ORID != 0 && item.products.PTID != allOrderFieldsCompleteProductItem.products.PTID)
                        {
                            //直接新增一筆包含ORID、PTID、數量三個欄位的資料行到OrderDetail資料表
                            item.orders.ORID = allOrderFieldsCompleteProductItem.orders.ORID;
                            orderDetails = new OrderDetails()
                            {
                                ORID = item.orders.ORID,
                                PTID = item.products.PTID,
                                SNID = item.specifications.SNID,
                                Quantity = item.RequireQuantities
                            };
                            await _context.OrderDetails.AddAsync(orderDetails);
                            await _context.SaveChangesAsync();

                            //在Products資料表將當前迴圈處理商品的已售數量加計需求數量
                            productDataLineGetFromTable = await _context.Products.FirstOrDefaultAsync(p => p.PTID == orderDetails.PTID);
                            productDataLineGetFromTable.SoldQuantity += item.RequireQuantities;
                            _context.Products.Update(productDataLineGetFromTable);
                            await _context.SaveChangesAsync();
                        }
                    }
                    continue;
                }

                orders = new Orders()
                {
                    OrderDate = item.orders.OrderDate,
                    SVID = item.sellerShipViaOptions,
                    CRID = item.orders.CRID,
                    ASID = item.orders.ASID,
                    PCID = string.IsNullOrEmpty(item.customerPaymentCardOptions) ? (short?)null : short.Parse(item.customerPaymentCardOptions),
                    SRID = item.products.SRID,
                    PMID = item.sellerPaymentMethodOptions,
                    CNID = item.orders.CNID
                };
                await _context.Orders.AddAsync(orders);
                await _context.SaveChangesAsync();
                //這個值將會在同一商家不同商品的情境中，使用到
                item.orders.ORID = orders.ORID;

                //Orders資料表新增後，取出identity生成的主鍵，再填到OrderDetails資料表當外鍵用
                orderDetails = new OrderDetails()
                {
                    ORID = orders.ORID,
                    PTID = item.products.PTID,
                    SNID = item.specifications.SNID,
                    Quantity = item.RequireQuantities
                };
                await _context.OrderDetails.AddAsync(orderDetails);
                await _context.SaveChangesAsync();

                //在Products資料表將當前迴圈處理商品的已售數量加計需求數量
                productDataLineGetFromTable = await _context.Products.FirstOrDefaultAsync(p => p.PTID == orderDetails.PTID);
                productDataLineGetFromTable.SoldQuantity += item.RequireQuantities;
                _context.Products.Update(productDataLineGetFromTable);
                await _context.SaveChangesAsync();
                if(orders.CNID != null)
                {
                    //在CustomerCoupons資料表訂單將使用掉的消費者折價券押上使用日期
                    customerCouponDataLineGetFromTable = await _context.CustomerCoupons.FirstOrDefaultAsync(c => c.CNID == orders.CNID && c.CRID == orders.CRID);
                    customerCouponDataLineGetFromTable.AppliedDate = orders.OrderDate;
                    _context.CustomerCoupons.Update(customerCouponDataLineGetFromTable);
                    await _context.SaveChangesAsync();
                }

            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}