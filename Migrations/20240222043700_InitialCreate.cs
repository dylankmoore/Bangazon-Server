using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bangazon.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SellerId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ImageURL = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirebaseKey = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    IsSeller = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "integer", nullable: false),
                    ProductsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrdersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Books" },
                    { 2, "Music" },
                    { 3, "Games" },
                    { 4, "Home" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "DateCreated", "IsOpen", "PaymentTypeId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 2, 21, 22, 37, 0, 69, DateTimeKind.Local).AddTicks(5308), true, 1 },
                    { 2, 2, new DateTime(2024, 2, 21, 22, 37, 0, 69, DateTimeKind.Local).AddTicks(5352), true, 2 },
                    { 3, 3, new DateTime(2024, 2, 21, 22, 37, 0, 69, DateTimeKind.Local).AddTicks(5354), false, 3 },
                    { 4, 4, new DateTime(2024, 2, 21, 22, 37, 0, 69, DateTimeKind.Local).AddTicks(5424), true, 4 }
                });

            migrationBuilder.InsertData(
                table: "PaymentType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Credit Card" },
                    { 2, "Debit Card" },
                    { 3, "Apple Pay" },
                    { 4, "Paypal" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageURL", "Name", "Price", "SellerId" },
                values: new object[,]
                {
                    { 1, 1, "Book of personal essays", "https://m.media-amazon.com/images/I/41fm2uwWJUL._SY445_SX342_.jpg", "The White Album by Joan Didion", 13m, 1 },
                    { 2, 2, "Audio CD", "https://m.media-amazon.com/images/I/71rRNAnVW6L._SL1400_.jpg", "Badmotorfinger by Soundgarden", 10m, 2 },
                    { 3, 4, "Guessing Game", "https://m.media-amazon.com/images/I/81MBgtB-Y8L._AC_SL1500_.jpg", "Taboo", 14m, 3 },
                    { 4, 4, "Multi-colored cutting board", "https://fredericksandmae.com/cdn/shop/products/0511_1296x.jpg?v=1631229005", "Confetti Cutting Board", 70m, 4 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "FirebaseKey", "FirstName", "IsSeller", "LastName", "UserName" },
                values: new object[,]
                {
                    { 1, "1675 E Altadena Dr, Altadena, CA", "brandonwalsh74@gmail.com", "npAVsfejgPZyg1q0OEKHq6l9zur2", "Brandon", false, "Walsh", "branman" },
                    { 2, "3959 Longridge Ave, Sherman Oaks, CA", "kelltaylor@hotmail.com", "npAVsfejgPZyg1q0OEKHq6l9zur2", "Kelly", true, "Taylor", "kells90210" },
                    { 3, "1605 E. Altadena Dr, Altadena, CA", "dmckay74@aol.com", "npAVsfejgPZyg1q0OEKHq6l9zur2", "Dylan", false, "McKay", "dmckay" },
                    { 4, "1060 Brooklawn Dr., Bel Air, CA", "dmartin@gmail.com", "npAVsfejgPZyg1q0OEKHq6l9zur2", "Donna", false, "Martin", "donnaloves2shop" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductsId",
                table: "OrderProduct",
                column: "ProductsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
