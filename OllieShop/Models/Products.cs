using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class Products
{
    public long PTID { get; set; }

    public string Name { get; set; } = null!;

    public decimal DeliveryFee { get; set; }

    public DateTime LaunchDate { get; set; }

    public bool Hidden { get; set; }

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
