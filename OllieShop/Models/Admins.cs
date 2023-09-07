using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Admins
{
    [Display(Name ="編號")]
    public short ADID { get; set; }

    [Display(Name = "管理員帳號")]
    [Required(ErrorMessage ="必填欄位")]
    public string Account { get; set; } = null!;

    [Display(Name = "密碼")]
    [Required(ErrorMessage = "必填欄位")]
    public string Password { get; set; } = null!;

    [Display(Name = "啟用日期")]
    [Required(ErrorMessage = "必填欄位")]
    [DataType(DataType.DateTime)]
    public DateTime EnableDate { get; set; }

    [Display(Name = "停用日期")]
    [DataType(DataType.DateTime)]
    public DateTime? DisableDate { get; set; }

    public virtual ICollection<Announcements> Announcements { get; set; } = new List<Announcements>();

    public virtual ICollection<Categorys> Categorys { get; set; } = new List<Categorys>();

    public virtual ICollection<ShipVias> ShipVias { get; set; } = new List<ShipVias>();

    public virtual ICollection<Violations> Violations { get; set; } = new List<Violations>();
}
