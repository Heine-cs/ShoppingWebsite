using System.ComponentModel.DataAnnotations;

namespace OllieShop.ViewModels
{
    public class VMProductWithSpecification
    {
        //From Products Model
        [Display(Name = "編號")]
        public long PTID { get; set; }

        [Display(Name = "名稱")]
        [StringLength(100, ErrorMessage = "商品名稱不得超過100個字")]
        [Required(ErrorMessage = "必填欄位")]
        public string Name { get; set; } = null!;

        [Display(Name = "宅配運費")]
        [Required(ErrorMessage = "必填欄位")]
        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal DeliveryFee { get; set; }

        [Display(Name = "上架時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日 hh時:mm分:ss秒}")]
        public DateTime LaunchDate { get; set; }

        [Display(Name = "停售")]
        [Required(ErrorMessage = "必填欄位")]
        public bool Hidden { get; set; }


        [Display(Name = "上鎖情況")]
        [Required(ErrorMessage = "必填欄位")]
        public bool Locked { get; set; }

        [Display(Name = "允許報價")]
        [Required(ErrorMessage = "必填欄位")]
        public bool Inquired { get; set; }

        [Display(Name = "允許分期")]
        [Required(ErrorMessage = "必填欄位")]
        public bool Installment { get; set; }

        [Display(Name = "是否全新")]
        [Required(ErrorMessage = "必填欄位")]
        public bool Unopened { get; set; }

        [Display(Name = "售價")]
        [Required(ErrorMessage = "必填欄位")]
        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "上架量")]
        [Required(ErrorMessage = "必填欄位")]
        public int ShelfQuantity { get; set; }

        [Display(Name = "已售量")]
        public int SoldQuantity { get; set; }

        [Display(Name = "描述")]
        [Required(ErrorMessage = "必填欄位")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "描述欄位不得超過500個字")]
        public string Description { get; set; } = null!;

        [Display(Name = "類別名稱")]
        public string? CYID { get; set; }

        [Display(Name = "商家編號")]
        public long? SRID { get; set; }

        //From Specifications Model
        [Display(Name = "編號")]
        public long SNID { get; set; }

        [Display(Name = "規格名稱")]
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(50, ErrorMessage = "名稱不得超過50個字")]
        public string SpecName { get; set; } = null!;


        [Display(Name = "圖片")]
        [Required(ErrorMessage = "請上傳圖片一張")]
        [StringLength(500)]
        public string Picture { get; set; } = null!;

        [Display(Name = "重量(單位:KG)")]
        [Required(ErrorMessage = "必填欄位")]
        public double Weight { get; set; }

        [Display(Name = "體積(單位:cm³)")]
        [Required(ErrorMessage = "必填欄位")]
        public double Size { get; set; }

        [Display(Name = "備貨日(單位:天)")]
        [Required(ErrorMessage = "必填欄位")]
        public int LeadDay { get; set; }

        [Display(Name = "包裝體積(單位:cm³)")]
        [Required(ErrorMessage = "必填欄位")]
        public double PackageSize { get; set; }

        [Display(Name = "贈品名稱")]
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(50, ErrorMessage = "贈品名稱不得超過50個字")]
        public string Freebie { get; set; } = null!;
    }
}
