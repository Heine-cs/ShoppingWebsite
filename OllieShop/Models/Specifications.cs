using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Specifications
{
    [Display(Name = "編號")]
    public long SNID { get; set; }

    [Display(Name = "名稱")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(50,ErrorMessage = "名稱不得超過50個字")]
    public string Name { get; set; } = null!;

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

    [Display(Name = "備貨日(單位:天")]
    [Required(ErrorMessage = "必填欄位")]
    public int LeadDay { get; set; }

    [Display(Name = "包裝體積(單位:cm³)")]
    [Required(ErrorMessage = "必填欄位")]
    public double PackageSize { get; set; }

    [Display(Name = "贈品")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(50,ErrorMessage = "贈品名稱不得超過50個字")]
    public string Freebie { get; set; } = null!;

    public long? PTID { get; set; }

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    public virtual Products? PT { get; set; }
}
