﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bangazon.Migrations
{
    [DbContext(typeof(BangazonDbContext))]
    partial class BangazonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bangazon.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Books"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Music"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Games"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Home"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Movies & TV"
                        });
                });

            modelBuilder.Entity("Bangazon.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            DateCreated = new DateTime(2024, 3, 4, 0, 14, 9, 433, DateTimeKind.Local).AddTicks(8969),
                            IsOpen = true,
                            PaymentTypeId = 1
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 2,
                            DateCreated = new DateTime(2024, 3, 4, 0, 14, 9, 433, DateTimeKind.Local).AddTicks(9038),
                            IsOpen = true,
                            PaymentTypeId = 2
                        },
                        new
                        {
                            Id = 3,
                            CustomerId = 3,
                            DateCreated = new DateTime(2024, 3, 4, 0, 14, 9, 433, DateTimeKind.Local).AddTicks(9043),
                            IsOpen = false,
                            PaymentTypeId = 3
                        },
                        new
                        {
                            Id = 4,
                            CustomerId = 4,
                            DateCreated = new DateTime(2024, 3, 4, 0, 14, 9, 433, DateTimeKind.Local).AddTicks(9048),
                            IsOpen = true,
                            PaymentTypeId = 4
                        });
                });

            modelBuilder.Entity("Bangazon.Models.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaymentType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Credit Card"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Debit Card"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Apple Pay"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Paypal"
                        });
                });

            modelBuilder.Entity("Bangazon.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ImageURL")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("SellerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SellerId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Book of personal essays",
                            ImageURL = "https://m.media-amazon.com/images/I/41fm2uwWJUL._SY445_SX342_.jpg",
                            Name = "The White Album by Joan Didion",
                            Price = 13m,
                            SellerId = 3
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "Audio CD",
                            ImageURL = "https://m.media-amazon.com/images/I/71rRNAnVW6L._SL1400_.jpg",
                            Name = "Badmotorfinger by Soundgarden",
                            Price = 10m,
                            SellerId = 2
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Description = "Guessing Game",
                            ImageURL = "https://m.media-amazon.com/images/I/81MBgtB-Y8L._AC_SL1500_.jpg",
                            Name = "Taboo",
                            Price = 14m,
                            SellerId = 3
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 4,
                            Description = "Froot Loop cereal bowl as a fun candle!",
                            ImageURL = "https://m.media-amazon.com/images/I/71kVczcRfdL._AC_SL1500_.jpg",
                            Name = "Cereal Bowl Candle",
                            Price = 35m,
                            SellerId = 2
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 5,
                            Description = "DVD",
                            ImageURL = "https://m.media-amazon.com/images/I/91ULKyUQWlL._SX425_.jpg",
                            Name = "Beverly Hills 90210: The Complete Series",
                            Price = 100m,
                            SellerId = 2
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 2,
                            Description = "Audio CD",
                            ImageURL = "https://m.media-amazon.com/images/I/51G92BBD5EL.jpg",
                            Name = "Now That's What I Call Music",
                            Price = 20m,
                            SellerId = 3
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 3,
                            Description = "Board Game",
                            ImageURL = "https://m.media-amazon.com/images/I/71Jr69W+MpL._AC_SL1500_.jpg",
                            Name = "Scrabble",
                            Price = 30m,
                            SellerId = 2
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 4,
                            Description = "Potted Plant",
                            ImageURL = "https://m.media-amazon.com/images/I/61PUzl5Z1BL._AC_SL1500_.jpg",
                            Name = "Pilea Plant",
                            Price = 25m,
                            SellerId = 3
                        });
                });

            modelBuilder.Entity("Bangazon.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsSeller")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "1675 E Altadena Dr, Altadena, CA",
                            Email = "brandonwalsh74@gmail.com",
                            FirstName = "Brandon",
                            IsSeller = false,
                            LastName = "Walsh",
                            Uid = "npAVsfejgPZyg1q0OEKHq6l9zur2",
                            UserName = "branman"
                        },
                        new
                        {
                            Id = 2,
                            Address = "3959 Longridge Ave, Sherman Oaks, CA",
                            Email = "kelltaylor@hotmail.com",
                            FirstName = "Kelly",
                            IsSeller = true,
                            LastName = "Taylor",
                            Uid = "fbkey2",
                            UserName = "kells90210"
                        },
                        new
                        {
                            Id = 3,
                            Address = "1605 E. Altadena Dr, Altadena, CA",
                            Email = "dmckay74@aol.com",
                            FirstName = "Dylan",
                            IsSeller = true,
                            LastName = "McKay",
                            Uid = "fbkey3",
                            UserName = "dmckay"
                        },
                        new
                        {
                            Id = 4,
                            Address = "1060 Brooklawn Dr., Bel Air, CA",
                            Email = "dmartin@gmail.com",
                            FirstName = "Donna",
                            IsSeller = false,
                            LastName = "Martin",
                            Uid = "fbkey4",
                            UserName = "donnaloves2shop"
                        });
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.Property<int>("OrdersId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductsId")
                        .HasColumnType("integer");

                    b.HasKey("OrdersId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("Bangazon.Models.Product", b =>
                {
                    b.HasOne("Bangazon.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bangazon.Models.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.HasOne("Bangazon.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bangazon.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bangazon.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
