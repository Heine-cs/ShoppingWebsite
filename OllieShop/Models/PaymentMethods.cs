using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class PaymentMethods
{
    [Display(Name ="編號")]
    public string PMID { get; set; } = null!;

    [Display(Name = "名稱")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(50,ErrorMessage ="名稱不能超過50個字")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual ICollection<SellerPaymentMethods> SellerPaymentMethods { get; set; } = new List<SellerPaymentMethods>();
}
