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
    public decimal UnitPrice { get; set; }

    [Display(Name = "上架量")]
    [Required(ErrorMessage = "必填欄位")]
    public int ShelfQuantity { get; set; }

    [Display(Name = "已售量")]
    public int SoldQuantity { get; set; }

    [Display(Name = "描述")]
    [Required(ErrorMessage = "必填欄位")]
    [DataType(DataType.MultilineText)]
    [StringLength(500,ErrorMessage = "描述欄位不得超過500個字")]
    public string Description { get; set; } = null!;

    [Display(Name = "類別編號")]
    [StringLength(5,ErrorMessage = "類別編號必須5個字")]
    [MinLength(5,ErrorMessage = "類別編號必須5個字")]
    public string? CYID { get; set; }

    [Display(Name = "商家編號")]
    public long? SRID { get; set; }

    public virtual Categorys? CY { get; set; }

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    public virtual Sellers? SR { get; set; }

    public virtual ICollection<Specifications> Specifications { get; set; } = new List<Specifications>();
}
