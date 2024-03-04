using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bangazon.Migrations
{
    public partial class ProductsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 3, 1, 4, 12, 4, 222, DateTimeKind.Local).AddTicks(8677));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 3, 1, 4, 12, 4, 222, DateTimeKind.Local).AddTicks(8724));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 3, 1, 4, 12, 4, 222, DateTimeKind.Local).AddTicks(8726));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 3, 1, 4, 12, 4, 222, DateTimeKind.Local).AddTicks(8728));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Froot Loop cereal bowl as a fun candle!", "https://m.media-amazon.com/images/I/71kVczcRfdL._AC_SL1500_.jpg", "Cereal Bowl Candle", 35m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CategoryId",
                value: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 3, 1, 3, 40, 56, 743, DateTimeKind.Local).AddTicks(9533));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 3, 1, 3, 40, 56, 743, DateTimeKind.Local).AddTicks(9612));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 3, 1, 3, 40, 56, 743, DateTimeKind.Local).AddTicks(9617));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 3, 1, 3, 40, 56, 743, DateTimeKind.Local).AddTicks(9621));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageURL", "Name", "Price" },
                values: new object[] { "Multi-colored cutting board", "https://fredericksandmae.com/cdn/shop/products/0511_1296x.jpg?v=1631229005", "Confetti Cutting Board", 70m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CategoryId",
                value: 1);
        }
    }
}
