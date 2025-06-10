using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DiamondAssessmentSystem.Infrastructure.Models;

public partial class DiamondAssessmentDbContext : DbContext
{
    public DiamondAssessmentDbContext()
    {
    }

    public DiamondAssessmentDbContext(DbContextOptions<DiamondAssessmentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<ChatLog> ChatLogs { get; set; }

    public virtual DbSet<CommitmentRecord> CommitmentRecords { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<SealingRecord> SealingRecords { get; set; }

    public virtual DbSet<ServicePrice> ServicePrices { get; set; }

    public virtual DbSet<ServicePriceAudit> ServicePriceAudits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=DiamondAssessmentSystem;User ID=sa;Password=12345;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blogs__2975AA28B2BEC880");

            entity.Property(e => e.BlogId).HasColumnName("blog_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Blogs__employee___787EE5A0");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__E2256D31278BC850");

            entity.Property(e => e.CertificateId).HasColumnName("certificate_id");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.ResultId).HasColumnName("result_id");

            entity.HasOne(d => d.Result).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.ResultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Certifica__resul__571DF1D5");
        });

        modelBuilder.Entity<ChatLog>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__ChatLogs__FD040B17EB659B19");

            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.MessageType)
                .HasMaxLength(50)
                .HasColumnName("message_type");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Customer).WithMany(p => p.ChatLogs)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__ChatLogs__custom__7B5B524B");

            entity.HasOne(d => d.Employee).WithMany(p => p.ChatLogs)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ChatLogs__employ__7C4F7684");

            entity.HasOne(d => d.Request).WithMany(p => p.ChatLogs)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__ChatLogs__reques__7D439ABD");
        });

        modelBuilder.Entity<CommitmentRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Commitme__BFCFB4DD80D73091");

            entity.ToTable("Commitment_records");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
            entity.Property(e => e.ApprovedDate)
                .HasColumnType("datetime")
                .HasColumnName("approved_date");
            entity.Property(e => e.CommitDate).HasColumnName("commit_date");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.CommitmentRecords)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__Commitmen__appro__5AEE82B9");

            entity.HasOne(d => d.Request).WithMany(p => p.CommitmentRecords)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Commitmen__reque__59FA5E80");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB857F89D667");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Idcard)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("IDcard");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TaxCode)
                .HasMaxLength(20)
                .HasColumnName("tax_code");
            entity.Property(e => e.UnitName)
                .HasMaxLength(50)
                .HasColumnName("unit_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C52E0BA8A4BD7B5A");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__4659622956576F92");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CommitmentId).HasColumnName("commitment_id");
            entity.Property(e => e.ConsultantId).HasColumnName("consultant_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
            entity.Property(e => e.SealingId).HasColumnName("sealing_id");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("service_type");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_price");

            entity.HasOne(d => d.Commitment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CommitmentId)
                .HasConstraintName("FK__Orders__commitme__6D0D32F4");

            entity.HasOne(d => d.Consultant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ConsultantId)
                .HasConstraintName("FK__Orders__consulta__6A30C649");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__customer__693CA210");

            entity.HasOne(d => d.Receipt).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ReceiptId)
                .HasConstraintName("FK__Orders__receipt___6B24EA82");

            entity.HasOne(d => d.Sealing).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SealingId)
                .HasConstraintName("FK__Orders__sealing___6C190EBB");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__3C5A4080575C81A9");

            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.IsAccepted).HasColumnName("is_accepted");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("service_type");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__order__6FE99F9F");

            entity.HasOne(d => d.Result).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ResultId)
                .HasConstraintName("FK__OrderDeta__resul__71D1E811");

            entity.HasOne(d => d.Service).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__servi__70DDC3D8");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__ED1FC9EA98AE5602");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.Method)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("method");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__order___74AE54BC");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PK__Receipts__91F52C1FCEE71648");

            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
            entity.Property(e => e.OrdCarat)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ord_carat");
            entity.Property(e => e.OrdColor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ord_color");
            entity.Property(e => e.OrdCut)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ord_cut");
            entity.Property(e => e.OrdOther)
                .HasMaxLength(255)
                .HasColumnName("ord_other");
            entity.Property(e => e.ReceiptDate).HasColumnName("receipt_date");
            entity.Property(e => e.RequestId).HasColumnName("request_id");

            entity.HasOne(d => d.Request).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Receipts__reques__5165187F");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__18D3B90F1B8A7815");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.RequestDate).HasColumnName("request_date");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("service_type");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Customer).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__custom__4D94879B");

            entity.HasOne(d => d.Employee).WithMany(p => p.Requests)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Requests__employ__4E88ABD4");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__Results__AFB3C316084A9156");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.CaratWeight)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("carat_weight");
            entity.Property(e => e.Clarity)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("clarity");
            entity.Property(e => e.Color)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Cut)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cut");
            entity.Property(e => e.DiamondId).HasColumnName("diamond_id");
            entity.Property(e => e.DiamondOrigin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("diamond_origin");
            entity.Property(e => e.Fluorescence)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fluorescence");
            entity.Property(e => e.Measurements)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("measurements");
            entity.Property(e => e.Polish)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("polish");
            entity.Property(e => e.Proportions)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("proportions");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.Shape)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("shape");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Symmetry)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("symmetry");

            entity.HasOne(d => d.Request).WithMany(p => p.Results)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Results__request__5441852A");
        });

        modelBuilder.Entity<SealingRecord>(entity =>
        {
            entity.HasKey(e => e.SealingId).HasName("PK__Sealing___B2156BB8B6D056FE");

            entity.ToTable("Sealing_records");

            entity.Property(e => e.SealingId).HasColumnName("sealing_id");
            entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
            entity.Property(e => e.ApprovedDate)
                .HasColumnType("datetime")
                .HasColumnName("approved_date");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.SealDate).HasColumnName("seal_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.SealingRecords)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__Sealing_r__appro__5EBF139D");

            entity.HasOne(d => d.Request).WithMany(p => p.SealingRecords)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sealing_r__reque__5DCAEF64");
        });

        modelBuilder.Entity<ServicePrice>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service___3E0DB8AFD13507E0");

            entity.ToTable("Service_prices");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("service_type");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Employee).WithMany(p => p.ServicePrices)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Service_p__emplo__619B8048");
        });

        modelBuilder.Entity<ServicePriceAudit>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__Service___5AF33E3363D6E7FA");

            entity.ToTable("Service_price_audit");

            entity.Property(e => e.AuditId).HasColumnName("audit_id");
            entity.Property(e => e.ActionType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("action_type");
            entity.Property(e => e.ChangeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("change_date");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.NewDuration).HasColumnName("new_duration");
            entity.Property(e => e.NewPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("new_price");
            entity.Property(e => e.OldDuration).HasColumnName("old_duration");
            entity.Property(e => e.OldPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("old_price");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("service_type");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Employee).WithMany(p => p.ServicePriceAudits)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Service_p__emplo__656C112C");

            entity.HasOne(d => d.Service).WithMany(p => p.ServicePriceAudits)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Service_p__servi__66603565");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
