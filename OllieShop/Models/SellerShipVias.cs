using Microsoft.EntityFrameworkCore;
using OllieShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OllieShop.Models
{
    [PrimaryKey(nameof(SRID), nameof(SVID))]
    public class SellerShipVias
    {
        [Column(Order = 0)]
        public long SRID { get; set; }

        [Column(Order = 1)]
        public string SVID { get; set; } = null!;

        [Display(Name = "取消綁定")]
        public bool Canceled { get; set; }

        [ForeignKey("SRID")]
        public virtual Sellers Sellers { get; set; }

        [ForeignKey("SVID")]
        public virtual ShipVias ShipVias { get; set; }

    }
}