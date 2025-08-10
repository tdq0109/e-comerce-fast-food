using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM.Migrations
{
    /// <inheritdoc />
    public partial class Ver_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 8, 15, 16, 25, 855, DateTimeKind.Local).AddTicks(9385));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 8, 15, 16, 25, 855, DateTimeKind.Local).AddTicks(9391));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Tags", "Topic" },
                values: new object[] { new DateTime(2025, 8, 8, 15, 16, 25, 855, DateTimeKind.Local).AddTicks(9396), "drink", "Mới" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 8, 15, 16, 26, 395, DateTimeKind.Local).AddTicks(3319), "$2a$11$89.4STyhlzjoM0p0m8OnOO7vsqoH7h0Fa8f4peRjYo9ig7Ol5/0du" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 8, 15, 16, 26, 122, DateTimeKind.Local).AddTicks(3754), "$2a$11$uhoFmhgYXDLHUtLwY8HJiOELy3wZBeP4LdCyNWDUh2OoQnrKXXXmy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 11, 12, 52, 562, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 11, 12, 52, 562, DateTimeKind.Local).AddTicks(2011));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Tags", "Topic" },
                values: new object[] { new DateTime(2025, 8, 7, 11, 12, 52, 562, DateTimeKind.Local).AddTicks(2016), null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 7, 11, 12, 53, 137, DateTimeKind.Local).AddTicks(3541), "$2a$11$LM1jEvFO3aEVGVYSVDn.yuVszs6GS1s/usLkRGuQZSpqklfFujXZe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 7, 11, 12, 52, 857, DateTimeKind.Local).AddTicks(9740), "$2a$11$UJ.np16B/mIpZwrrgEHuGOEGHa9nLkroz83QOC8qM8tX/EksmOmqK" });
        }
    }
}
