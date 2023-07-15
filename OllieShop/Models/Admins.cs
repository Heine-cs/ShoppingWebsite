using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Admins
{
    [Display(Name ="編號")]
    public short ADID { get; set; }

    [Display(Name = "帳號")]
    public string Account { get; set; } = null!;

    [Display(Name = "密碼")]
    public string Password { get; set; } = null!;

    [Display(Name = "啟用日期")]
    public DateTime EnableDate { get; set; }

    [Display(Name = "停用日期")]
    public DateTime? DisableDate { get; set; }

    public virtual ICollection<Announcements> Announcements { get; set; } = new List<Announcements>();

    public virtual ICollection<Categorys> Categorys { get; set; } = new List<Categorys>();

    public virtual ICollection<ShipVias> ShipVias { get; set; } = new List<ShipVias>();

    public virtual ICollection<Violations> Violations { get; set; } = new List<Violations>();
}
