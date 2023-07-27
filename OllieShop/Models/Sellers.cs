using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class Sellers
{
    public long SRID { get; set; }

    [Display(Name ="商鋪名稱")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(70, ErrorMessage = "商鋪名稱不得超過70個字")]
    public string ShopNAME { get; set; } = null!;

    [Display(Name = "統一編號")]
    [Required(ErrorMessage = "必填欄位")]
    [RegularExpression("[0-9無統一編號之業者]{8}", ErrorMessage = "統編長度為0到9的8個數字組合，請檢查是否漏填或填錯")]
    [StringLength(8, ErrorMessage = "統一編號長度僅為8碼之間的數字組合，請檢查是否漏填或填錯")]
    public string TaxID { get; set; } = null!;

    [Display(Name = "銀行局號")]
    [Required(ErrorMessage = "必填欄位")]
    [MinLength(3, ErrorMessage = "銀行局號長度僅為3碼之間的數字組合，請檢查是否漏填或填錯")]
    [MaxLength(3, ErrorMessage = "銀行局號長度僅為3碼之間的數字組合，請檢查是否漏填或填錯")]
    public string BankCode { get; set; } = null!;

    [Display(Name = "銀行帳號")]
    [Required(ErrorMessage = "必填欄位")]
    [MinLength(12, ErrorMessage = "銀行帳號長度落在12~14碼之間的數字組合，請檢查是否漏<br>填或填錯")]
    [StringLength(14, ErrorMessage = "銀行帳號長度落在12~14碼之間的數字組合，請檢查是否漏<br>填或填錯")]
    public string BankAccount { get; set; } = null!;

    public long? URID { get; set; }

    public virtual ICollection<Messages> Messages { get; set; } = new List<Messages>();

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual ICollection<Products> Products { get; set; } = new List<Products>();

    public virtual ICollection<SellerPaymentMethods> SellerPaymentMethods { get; set; } = new List<SellerPaymentMethods>();

    public virtual Users? UR { get; set; }

}
