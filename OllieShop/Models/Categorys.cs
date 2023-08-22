using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Categorys
{
    [Display(Name = "編號")]
    public string CYID { get; set; } = null!;

    [Display(Name = "類別名稱")]
    [Required(ErrorMessage = "必填欄位")]
    public string Name { get; set; } = null!;

    
    public short? ADID { get; set; }

    [Display(Name = "管理員編號")]
    public virtual Admins AD { get; set; }

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();
}
