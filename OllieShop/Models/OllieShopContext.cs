using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OllieShop.Models;

public partial class OllieShopContext : DbContext
{
    public OllieShopContext(DbContextOptions<OllieShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accounts> Accounts { get; set; }

    public virtual DbSet<Addresses> Addresses { get; set; }

    public virtual DbSet<Admins> Admins { get; set; }

    public virtual DbSet<Announcements> Announcements { get; set; }

    public virtual DbSet<Categorys> Categorys { get; set; }

    public virtual DbSet<Coupons> Coupons { get; set; }

    public virtual DbSet<CustomerCoupons> CustomerCoupons { get; set; }

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<Messages> Messages { get; set; }

    public virtual DbSet<OrderDetails> OrderDetails { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<PaymentCards> PaymentCards { get; set; }

    public virtual DbSet<PaymentMethods> PaymentMethods { get; set; }

    public virtual DbSet<Products> Products { get; set; }

    public virtual DbSet<SellerPaymentMethods> SellerPaymentMethods { get; set; }

    public virtual DbSet<Sellers> Sellers { get; set; }

    public virtual DbSet<ShipVias> ShipVias { get; set; }

    public virtual DbSet<Specifications> Specifications { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<Violations> Violations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accounts>(entity =>
        {
            entity.HasKey(e => e.ACID).HasName("PK__Accounts__06FECA7968530790");

            entity.Property(e => e.Account)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Level)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.UR).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.URID)
                .HasConstraintName("FK__Accounts__URID__412EB0B6");
        });

        modelBuilder.Entity<Addresses>(entity =>
        {
            entity.HasKey(e => e.ASID).HasName("PK__Addresse__4DF619604C049667");

            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.District).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.Street).HasMaxLength(30);

            entity.HasOne(d => d.UR).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.URID)
                .HasConstraintName("FK__Addresses__URID__440B1D61");
        });

        modelBuilder.Entity<Admins>(entity =>
        {
            entity.HasKey(e => e.ADID).HasName("PK__Admins__7930D5A0A249A12F");

            entity.Property(e => e.Account)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.DisableDate).HasColumnType("datetime");
            entity.Property(e => e.EnableDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Announcements>(entity =>
        {
            entity.HasKey(e => e.ATID).HasName("PK__Announce__4D3800A977D2B549");

            entity.Property(e => e.PublicDate).HasColumnType("datetime");

            entity.HasOne(d => d.AD).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.ADID)
                .HasConstraintName("FK__Announceme__ADID__398D8EEE");
        });

        modelBuilder.Entity<Categorys>(entity =>
        {
            entity.HasKey(e => e.CYID).HasName("PK__Category__A2E332A8512B7437");

            entity.Property(e => e.CYID)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.AD).WithMany(p => p.Categorys)
                .HasForeignKey(d => d.ADID)
                .HasConstraintName("FK__Categorys__ADID__3C69FB99");
        });

        modelBuilder.Entity<Coupons>(entity =>
        {
            entity.HasKey(e => e.CNID).HasName("PK__Coupons__AA570FD4FF288735");

            entity.Property(e => e.CODE).HasMaxLength(50);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CustomerCoupons>(entity =>
        {
            entity.HasKey(e => e.CRCNID).HasName("PK__Customer__A7E33CF83C3F30FF");

            entity.Property(e => e.AppliedDate).HasColumnType("datetime");
            entity.Property(e => e.DateAdded).HasColumnType("datetime");

            entity.HasOne(d => d.CN).WithMany(p => p.CustomerCoupons)
                .HasForeignKey(d => d.CNID)
                .HasConstraintName("FK__CustomerCo__CNID__68487DD7");

            entity.HasOne(d => d.CR).WithMany(p => p.CustomerCoupons)
                .HasForeignKey(d => d.CRID)
                .HasConstraintName("FK__CustomerCo__CRID__693CA210");
        });

        modelBuilder.Entity<Customers>(entity =>
        {
            entity.HasKey(e => e.CRID).HasName("PK__Customer__F2363F52414C186F");


            //修復為一對一關連
            entity.HasOne(c => c.UR)
            .WithOne()
            .HasForeignKey<Customers>(c => c.URID)
            .HasConstraintName("FK__Customers__URID__4BAC3F29");

            //dbfirst產生的一對多關連
            //entity.HasOne(d => d.UR).WithMany(p => p.Customers)
            //    .HasForeignKey(d => d.URID)
            //    .HasConstraintName("FK__Customers__URID__4BAC3F29");
        });

        modelBuilder.Entity<Messages>(entity =>
        {
            entity.HasKey(e => e.MEID).HasName("PK__Messages__1A36DA7AD7A66764");

            entity.Property(e => e.MEContent).HasMaxLength(500);
            entity.Property(e => e.PostDate).HasColumnType("datetime");

            entity.HasOne(d => d.CR).WithMany(p => p.Messages)
                .HasForeignKey(d => d.CRID)
                .HasConstraintName("FK__Messages__CRID__7B5B524B");

            entity.HasOne(d => d.SR).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SRID)
                .HasConstraintName("FK__Messages__SRID__7C4F7684");
        });

        modelBuilder.Entity<OrderDetails>(entity =>
        {
            entity.HasKey(e => new { e.ORID, e.PTID, e.SNID }).HasName("PK__OrderDet__18C3C33F8AFB4232");

            entity.HasOne(d => d.OR).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ORID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDetai__ORID__76969D2E");

            entity.HasOne(d => d.PT).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.PTID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDetai__PTID__778AC167");

            entity.HasOne(d => d.SN).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.SNID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDetai__SNID__787EE5A0");
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.ORID).HasName("PK__Orders__A9A8BCD6B5645981");

            entity.Property(e => e.ArrivalDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.PMID)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.SVID)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ShippedDate).HasColumnType("datetime");

            entity.HasOne(d => d.AS).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ASID)
                .HasConstraintName("FK__Orders__ASID__6FE99F9F");

            entity.HasOne(d => d.CN).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CNID)
                .HasConstraintName("FK__Orders__CNID__73BA3083");

            entity.HasOne(d => d.CR).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CRID)
                .HasConstraintName("FK__Orders__CRID__6EF57B66");

            entity.HasOne(d => d.PC).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PCID)
                .HasConstraintName("FK__Orders__PCID__70DDC3D8");

            entity.HasOne(d => d.PM).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PMID)
                .HasConstraintName("FK__Orders__PMID__72C60C4A");

            entity.HasOne(d => d.SR).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SRID)
                .HasConstraintName("FK__Orders__SRID__71D1E811");

            entity.HasOne(d => d.SV).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SVID)
                .HasConstraintName("FK__Orders__SVID__6E01572D");
        });

        modelBuilder.Entity<PaymentCards>(entity =>
        {
            entity.HasKey(e => e.PCID).HasName("PK__PaymentC__580221FF43E6BAE5");

            entity.Property(e => e.BillAddress).HasMaxLength(100);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Number)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SecurityCode)
                .HasMaxLength(4)
                .IsUnicode(false);

            entity.HasOne(d => d.CR).WithMany(p => p.PaymentCards)
                .HasForeignKey(d => d.CRID)
                .HasConstraintName("FK__PaymentCar__CRID__4E88ABD4");
        });

        modelBuilder.Entity<PaymentMethods>(entity =>
        {
            entity.HasKey(e => e.PMID).HasName("PK__PaymentM__5C86FF66C0804D4D");

            entity.Property(e => e.PMID)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Products>(entity =>
        {
            entity.HasKey(e => e.PTID).HasName("PK__Products__BCC07F4F85985E09");

            entity.Property(e => e.CYID)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DeliveryFee).HasColumnType("money");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.LaunchDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.CY).WithMany(p => p.Products)
                .HasForeignKey(d => d.CYID)
                .HasConstraintName("FK__Products__CYID__5629CD9C");

            entity.HasOne(d => d.SR).WithMany(p => p.Products)
                .HasForeignKey(d => d.SRID)
                .HasConstraintName("FK__Products__SRID__571DF1D5");
        });

        modelBuilder.Entity<SellerPaymentMethods>(entity =>
        {
            entity.HasKey(e => new { e.SRID, e.PMID }).HasName("PK__SellerPa__D55F7F5CC656575D");

            entity.Property(e => e.PMID)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.PM).WithMany(p => p.SellerPaymentMethods)
                .HasForeignKey(d => d.PMID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SellerPaym__PMID__628FA481");

            entity.HasOne(d => d.SR).WithMany(p => p.SellerPaymentMethods)
                .HasForeignKey(d => d.SRID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SellerPaym__SRID__619B8048");
        });

        modelBuilder.Entity<Sellers>(entity =>
        {
            entity.HasKey(e => e.SRID).HasName("PK__Sellers__A09710AA2CEC894B");

            entity.Property(e => e.BankAccount)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.BankCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ShopNAME).HasMaxLength(70);
            entity.Property(e => e.TaxID)
                .HasMaxLength(8)
                .IsFixedLength();

            //修復為一對一關連
            entity.HasOne(s => s.UR)
            .WithOne()
            .HasForeignKey<Sellers>(s => s.URID)
            .HasConstraintName("FK__Sellers__URID__5165187F");
            //dbfirst產生的一對多關連
            //entity.HasOne(d => d.UR).WithMany(p => p.Sellers)
            //    .HasForeignKey(d => d.URID)
            //    .HasConstraintName("FK__Sellers__URID__5165187F");
        });

        modelBuilder.Entity<ShipVias>(entity =>
        {
            entity.HasKey(e => e.SVID).HasName("PK__ShipVias__A189DAE719B0CAE7");

            entity.Property(e => e.SVID)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.AD).WithMany(p => p.ShipVias)
                .HasForeignKey(d => d.ADID)
                .HasConstraintName("FK__ShipVias__ADID__5CD6CB2B");
        });

        modelBuilder.Entity<Specifications>(entity =>
        {
            entity.HasKey(e => e.SNID).HasName("PK__Specific__A7781D5B072BCFA1");

            entity.Property(e => e.Freebie).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Picture)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.PT).WithMany(p => p.Specifications)
                .HasForeignKey(d => d.PTID)
                .HasConstraintName("FK__Specificat__PTID__59FA5E80");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.URID).HasName("PK__Users__AA3DE2DC4DDAB569");

            entity.Property(e => e.BirthDay).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);

            //設定一對一關聯(與Sellers)
            entity.HasOne(u => u.Sellers) // Users 資料模型的導航屬性
                .WithOne(s => s.UR)    // Sellers 資料模型的導航屬性，這裡不用指定，因為是單向一對一
                .HasForeignKey<Sellers>(s => s.URID) // 外鍵欄位，這裡使用 UserID 作為外鍵
                .HasConstraintName("FK__Sellers__URID__5165187F");

            //設定一對一關聯(與Customers)
            entity.HasOne(u => u.Customers) // Users 資料模型的導航屬性
                .WithOne(c => c.UR)    // Customers 資料模型的導航屬性，這裡不用指定，因為是單向一對一
                .HasForeignKey<Customers>(c => c.URID) // 外鍵欄位，這裡使用 URID 作為外鍵
                .HasConstraintName("FK__Customers__URID__4BAC3F29");
        });

        modelBuilder.Entity<Violations>(entity =>
        {
            entity.HasKey(e => e.VioID).HasName("PK__Violatio__C3C882165C437744");

            entity.Property(e => e.Reason).HasMaxLength(300);
            entity.Property(e => e.SubmitDate).HasColumnType("datetime");

            entity.HasOne(d => d.AD).WithMany(p => p.Violations)
                .HasForeignKey(d => d.ADID)
                .HasConstraintName("FK__Violations__ADID__48CFD27E");

            entity.HasOne(d => d.SubmitterNavigation).WithMany(p => p.ViolationsSubmitterNavigation)
                .HasForeignKey(d => d.Submitter)
                .HasConstraintName("FK__Violation__Submi__46E78A0C");

            entity.HasOne(d => d.SuspectNavigation).WithMany(p => p.ViolationsSuspectNavigation)
                .HasForeignKey(d => d.Suspect)
                .HasConstraintName("FK__Violation__Suspe__47DBAE45");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
