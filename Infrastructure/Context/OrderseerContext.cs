using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class OrderseerContext : DbContext
{
    public OrderseerContext()
    {
    }

    public OrderseerContext(DbContextOptions<OrderseerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_pkey");

            entity.ToTable("order");

            entity.HasIndex(e => e.Guid, "order_guid_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UserReference).HasColumnName("user_reference");
        });

        modelBuilder.Entity<OrderStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_status_history_pkey");

            entity.ToTable("order_status_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChangeDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("change_date");
            entity.Property(e => e.OldStatusId).HasColumnName("old_status_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.OldStatus).WithMany(p => p.OrderStatusHistoryOldStatuses)
                .HasForeignKey(d => d.OldStatusId)
                .HasConstraintName("order_status_history_old_status_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderStatusHistories)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_order");

            entity.HasOne(d => d.Status).WithMany(p => p.OrderStatusHistoryStatuses)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_status");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("status");

            entity.HasIndex(e => e.Name, "status_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}