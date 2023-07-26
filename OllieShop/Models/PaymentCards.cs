using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OllieShop.Models;

public partial class PaymentCards
{
    public short PCID { get; set; }

    [Display(Name ="到期日")]
    
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "必填欄位")]
    public DateTime ExpiryDate { get; set; }

    [Display(Name ="安全碼")]
    [StringLength(4,ErrorMessage ="卡片安全碼為3或4碼的數字組合，請檢查是否輸入錯誤或漏填")]
    [MinLength(3,ErrorMessage = "卡片安全碼為3或4碼的數字組合，請檢查是否輸入錯誤或漏填")]
    [Required(ErrorMessage = "必填欄位")]
    public string SecurityCode { get; set; } = null!;

    [Display(Name = "卡號")]
    [StringLength(16, ErrorMessage = "卡片號碼為16碼的數字組合，請檢查是否輸入錯誤或漏填")]
    [MinLength(16, ErrorMessage = "卡片號碼為16碼的數字組合，請檢查是否輸入錯誤或漏填")]
    [Required(ErrorMessage = "必填欄位")]
    public string Number { get; set; } = null!;


    [Display(Name = "帳單地址")]
    [StringLength(100, ErrorMessage = "地址長度不得超過100個字")]
    [Required(ErrorMessage = "必填欄位")]
    public string BillAddress { get; set; } = null!;

    public long? CRID { get; set; }

    public virtual Customers? CR { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
