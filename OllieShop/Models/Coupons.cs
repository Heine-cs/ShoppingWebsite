using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class Coupons
{
    public long CNID { get; set; }

    public string CODE { get; set; } = null!;

    public DateTime ExpiryDate { get; set; }

    public double Discount { get; set; }

    public virtual ICollection<CustomerCoupons> CustomerCoupons { get; set; } = new List<CustomerCoupons>();

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
