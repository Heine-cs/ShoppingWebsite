using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Coupons
{
    [Display(Name ="編號")]
    public long CNID { get; set; }

    [Display(Name = "代碼")]
    [Required(ErrorMessage ="必填欄位")]
    [StringLength(50,ErrorMessage ="代碼不能超過50個字")]
    public string CODE { get; set; } = null!;

    [Display(Name = "截止日")]
    [Required(ErrorMessage = "必填欄位")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日 hh時:mm分:ss秒}")]
    public DateTime ExpiryDate { get; set; }

    [Display(Name = "折扣")]
    [Required(ErrorMessage = "必填欄位")]
    public double Discount { get; set; }

    public virtual ICollection<CustomerCoupons> CustomerCoupons { get; set; } = new List<CustomerCoupons>();

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
