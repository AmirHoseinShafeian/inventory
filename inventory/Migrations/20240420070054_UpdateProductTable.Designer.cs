﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using inventory.DataAccess;

#nullable disable

namespace inventory.Migrations
{
    [DbContext(typeof(EFCoreContext))]
    [Migration("20240420070054_UpdateProductTable")]
    partial class UpdateProductTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("inventory.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Exp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("ProductGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductGroupId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("inventory.Entities.ProductGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentGroupId");

                    b.ToTable("productGroups");
                });

            modelBuilder.Entity("inventory.Entities.ProductRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataRegister")
                        .HasColumnType("datetime2");

                    b.Property<int>("PriceOfProduct")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductNumber")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("productRegistrations");
                });

            modelBuilder.Entity("inventory.Entities.ProductRemittance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataRegister")
                        .HasColumnType("datetime2");

                    b.Property<int>("PriceOfProduct")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductNumber")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("productRemittances");
                });

            modelBuilder.Entity("inventory.Entities.Product", b =>
                {
                    b.HasOne("inventory.Entities.ProductGroup", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductGroup");
                });

            modelBuilder.Entity("inventory.Entities.ProductGroup", b =>
                {
                    b.HasOne("inventory.Entities.ProductGroup", "ParentGroup")
                        .WithMany()
                        .HasForeignKey("ParentGroupId");

                    b.Navigation("ParentGroup");
                });

            modelBuilder.Entity("inventory.Entities.ProductRegistration", b =>
                {
                    b.HasOne("inventory.Entities.Product", "Product")
                        .WithMany("ProductRegistrations")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("inventory.Entities.ProductRemittance", b =>
                {
                    b.HasOne("inventory.Entities.Product", "Product")
                        .WithMany("ProductRemittances")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("inventory.Entities.Product", b =>
                {
                    b.Navigation("ProductRegistrations");

                    b.Navigation("ProductRemittances");
                });

            modelBuilder.Entity("inventory.Entities.ProductGroup", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}