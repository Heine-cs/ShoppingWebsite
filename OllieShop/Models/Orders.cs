using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class Orders
{
    public long ORID { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public DateTime? ArrivalDate { get; set; }

    public bool Canceled { get; set; }

    public bool Locked { get; set; }

    public string? SVID { get; set; }

    public long? CRID { get; set; }

    public long? ASID { get; set; }

    public short? PCID { get; set; }

    public long? SRID { get; set; }

    public string? PMID { get; set; }

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
