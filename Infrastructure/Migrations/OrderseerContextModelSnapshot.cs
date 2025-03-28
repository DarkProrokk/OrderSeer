﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(OrderseerContext))]
    partial class OrderseerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid")
                        .HasColumnName("guid");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer")
                        .HasColumnName("status_id");

                    b.HasKey("Id")
                        .HasName("order_pkey");

                    b.HasIndex("StatusId");

                    b.HasIndex(new[] { "Guid" }, "order_guid_key")
                        .IsUnique();

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderStatusHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ChangeDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("change_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int?>("OldStatusId")
                        .HasColumnType("integer")
                        .HasColumnName("old_status_id");

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer")
                        .HasColumnName("order_id");

                    b.Property<int?>("StatusId")
                        .HasColumnType("integer")
                        .HasColumnName("status_id");

                    b.HasKey("Id")
                        .HasName("order_status_history_pkey");

                    b.HasIndex("OldStatusId");

                    b.HasIndex("OrderId");

                    b.HasIndex("StatusId");

                    b.ToTable("order_status_history", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("status_pkey");

                    b.HasIndex(new[] { "Name" }, "status_name_key")
                        .IsUnique();

                    b.ToTable("status", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.HasOne("Domain.Entities.Status", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Domain.Entities.OrderStatusHistory", b =>
                {
                    b.HasOne("Domain.Entities.Status", "OldStatus")
                        .WithMany("OrderStatusHistoryOldStatuses")
                        .HasForeignKey("OldStatusId")
                        .HasConstraintName("order_status_history_old_status_id_fkey");

                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithMany("OrderStatusHistories")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("fk_order");

                    b.HasOne("Domain.Entities.Status", "Status")
                        .WithMany("OrderStatusHistoryStatuses")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("fk_status");

                    b.Navigation("OldStatus");

                    b.Navigation("Order");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderStatusHistories");
                });

            modelBuilder.Entity("Domain.Entities.Status", b =>
                {
                    b.Navigation("OrderStatusHistoryOldStatuses");

                    b.Navigation("OrderStatusHistoryStatuses");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
