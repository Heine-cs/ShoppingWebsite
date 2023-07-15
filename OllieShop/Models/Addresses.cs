using System;
using System.Collections.Generic;

namespace OllieShop.Models;

public partial class Addresses
{
    public long ASID { get; set; }

    public string District { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public long? URID { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual Users? UR { get; set; }
}
