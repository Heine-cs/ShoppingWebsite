using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Products
{
    [Display(Name = "編號")]
    public long PTID { get; set; }

    [Display(Name = "名稱")]
    [StringLength(100,ErrorMessage = "商品名稱不得超過100個字")]
    [Required(ErrorMessage = "必填欄位")]
    public string Name { get; set; } = null!;

    [Display(Name = "宅配運費")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal DeliveryFee { get; set; }

    [Display(Name = "上架時間")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日 hh時:mm分:ss秒}")]
    public DateTime LaunchDate { get; set; }

    [Display(Name = "在售情況")]
    [Required(ErrorMessage = "必填欄位")]
    public bool Hidden { get; set; }


    [Display(Name = "上鎖情況")]
    //[Required(ErrorMessage =)]
    public bool Locked { get; set; }

    public bool Inquired { get; set; }

    public bool Installment { get; set; }

    public bool Unopened { get; set; }

    public decimal UnitPrice { get; set; }

    public int ShelfQuantity { get; set; }

    public int SoldQuantity { get; set; }

    public string Description { get; set; } = null!;

    public string? CYID { get; set; }

    public long? SRID { get; set; }

    public virtual Categorys? CY { get; set; }

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    public virtual Sellers? SR { get; set; }

    public virtual ICollection<Specifications> Specifications { get; set; } = new List<Specifications>();
}
