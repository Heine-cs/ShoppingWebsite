using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class ShipVias
{
    [Display(Name="編號")]
    public string SVID { get; set; } = null!;

    [Display(Name = "名稱")]
    [StringLength(50, ErrorMessage ="名稱不得超過50個字")]
    [Required(ErrorMessage = "必填欄位")]
    public string Name { get; set; } = null!;

    [Display(Name = "管理員編號")]
    [Required(ErrorMessage = "必填欄位")]
    public short? ADID { get; set; }

    [Display(Name = "管理員帳號")]
    public virtual Admins? AD { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
