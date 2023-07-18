using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Addresses
{
    public long ASID { get; set; }

    [Display(Name ="區")]
    public string District { get; set; } = null!;

    [Display(Name = "詳細地址")]
    public string Street { get; set; } = null!;

    [Display(Name = "市/縣")]
    public string City { get; set; } = null!;

    [Display(Name = "手機號碼")]
    public string Phone { get; set; } = null!;

    public long? URID { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual Users? UR { get; set; }
}
