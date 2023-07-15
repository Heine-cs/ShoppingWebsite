using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class Customers
{
    public long CRID { get; set; }

    public long? URID { get; set; }

    public virtual ICollection<CustomerCoupons> CustomerCoupons { get; set; } = new List<CustomerCoupons>();

    public virtual ICollection<Messages> Messages { get; set; } = new List<Messages>();

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual ICollection<PaymentCards> PaymentCards { get; set; } = new List<PaymentCards>();

    public virtual Users? UR { get; set; }
}
