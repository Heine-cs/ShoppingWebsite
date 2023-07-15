using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class PaymentCards
{
    public short PCID { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string SecurityCode { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string BillAddress { get; set; } = null!;

    public long? CRID { get; set; }

    public virtual Customers? CR { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
