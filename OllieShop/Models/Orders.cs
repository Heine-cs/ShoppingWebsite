using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Orders
{
    [Display(Name ="編號")]
    public long ORID { get; set; }

    [Display(Name = "訂購日")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh點:mm分:ss秒}")]
    [DataType(DataType.DateTime)]
    public DateTime OrderDate { get; set; }

    [Display(Name = "付款日")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh點:mm分:ss秒}")]
    [DataType(DataType.DateTime)]
    public DateTime? PaymentDate { get; set; }

    [Display(Name = "出貨日")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh點:mm分:ss秒}")]
    [DataType(DataType.DateTime)]
    public DateTime? ShippedDate { get; set; }

    [Display(Name = "送達日")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh點:mm分:ss秒}")]
    [DataType(DataType.DateTime)]
    public DateTime? ArrivalDate { get; set; }

    [Display(Name = "訂單已取消")]
    public bool Canceled { get; set; }

    [Display(Name = "已鎖定")]
    public bool Locked { get; set; }

    [Display(Name = "配送方式編號")]
    public string? SVID { get; set; }

    [Display(Name = "消費者編號")]
    public long? CRID { get; set; }

    [Display(Name = "地址組編號")]
    public long? ASID { get; set; }

    [Display(Name = "信用卡編號")]
    public short? PCID { get; set; }

    [Display(Name = "商家編號")]
    public long? SRID { get; set; }

    [Display(Name = "付款方式編號")]
    public string? PMID { get; set; }

    [Display(Name = "折價券編號")]
    public long? CNID { get; set; }

    public virtual Addresses? AS { get; set; }

    public virtual Coupons? CN { get; set; }

    public virtual Customers? CR { get; set; }

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    public virtual PaymentCards? PC { get; set; }

    public virtual PaymentMethods? PM { get; set; }

    public virtual Sellers? SR { get; set; }

    public virtual ShipVias? SV { get; set; }
}
