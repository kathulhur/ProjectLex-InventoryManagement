﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectLex.InventoryManagement.Database.Data;

namespace ProjectLex.InventoryManagement.Database.Migrations
{
    [DbContext(typeof(InventoryManagementContext))]
    [Migration("20220202095340_Initial Migrate")]
    partial class InitialMigrate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Customer", b =>
                {
                    b.Property<Guid>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerFirstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerLastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StaffID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CustomerID");

                    b.HasIndex("StaffID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Defective", b =>
                {
                    b.Property<Guid>("DefectiveID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateDeclared")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("DefectiveID");

                    b.HasIndex("ProductID");

                    b.ToTable("Defectives");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Location", b =>
                {
                    b.Property<Guid>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Order", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeliveryStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OrderTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.OrderDetail", b =>
                {
                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("OrderDetailAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OrderDetailQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProductID", "OrderID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductAvailability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.Property<string>("ProductSKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SupplierID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.ProductLocation", b =>
                {
                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LocationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProductID", "LocationID");

                    b.HasIndex("LocationID");

                    b.ToTable("ProductLocations");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Role", b =>
                {
                    b.Property<Guid>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Staff", b =>
                {
                    b.Property<Guid>("StaffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StaffAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffUsername")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffID");

                    b.HasIndex("RoleID");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Supplier", b =>
                {
                    b.Property<Guid>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SupplierAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Warehouse", b =>
                {
                    b.Property<Guid>("WarehouseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WarehouseAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehouseEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehouseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehousePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("WarehouseVat")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("WarehouseID");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Customer", b =>
                {
                    b.HasOne("ProjectLex.InventoryManagement.Database.Models.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Defective", b =>
                {
                    b.HasOne("ProjectLex.InventoryManagement.Database.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Order", b =>
                {
                    b.HasOne("ProjectLex.InventoryManagement.Database.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.OrderDetail", b =>
                {
                    b.HasOne("ProjectLex.InventoryManagement.Database.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectLex.InventoryManagement.Database.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Product", b =>
                {
                    b.HasOne("ProjectLex.InventoryManagement.Database.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectLex.InventoryManagement.Database.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.ProductLocation", b =>
                {
                    b.HasOne("ProjectLex.InventoryManagement.Database.Models.Location", "Location")
                        .WithMany("ProductLocations")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectLex.InventoryManagement.Database.Models.Product", "Product")
                        .WithMany("ProductLocations")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Staff", b =>
                {
                    b.HasOne("ProjectLex.InventoryManagement.Database.Models.Role", "Role")
                        .WithMany("Staffs")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Location", b =>
                {
                    b.Navigation("ProductLocations");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Product", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("ProductLocations");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Role", b =>
                {
                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("ProjectLex.InventoryManagement.Database.Models.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
