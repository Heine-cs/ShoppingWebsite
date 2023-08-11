using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class CustomerCoupons
{
    [Display(Name ="編號")]
    public long CRCNID { get; set; }

    [Display(Name = "登錄票券日")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日 hh時:mm分:ss秒}")]
    public DateTime DateAdded { get; set; }

    [Display(Name ="使用日")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日 hh時:mm分:ss秒}")]
    public DateTime? AppliedDate { get; set; }

    [Display(Name ="折價券編號")]
    public long? CNID { get; set; }

    [Display(Name = "消費者編號")]
    public long? CRID { get; set; }

    public virtual Coupons? CN { get; set; }

    public virtual Customers? CR { get; set; }
}
