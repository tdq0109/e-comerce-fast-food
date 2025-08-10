using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM.Migrations
{
    /// <inheritdoc />
    public partial class AddNoteToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Orders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 8, 23, 26, 36, 693, DateTimeKind.Local).AddTicks(3730));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 8, 23, 26, 36, 693, DateTimeKind.Local).AddTicks(3735));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 8, 23, 26, 36, 693, DateTimeKind.Local).AddTicks(3740));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 8, 23, 26, 37, 234, DateTimeKind.Local).AddTicks(5906), "$2a$11$g2UhoMctMQSPaAgXcLET.e78wQ3ob1JBOQSjxSDk/vruQmgo5PDmO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 8, 23, 26, 36, 968, DateTimeKind.Local).AddTicks(489), "$2a$11$Rq2HoV.SwHNFcFxaJq8kmefwCB0XC43030AE4/UFDqpSo8EjIHijG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Orders");

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
                column: "CreatedAt",
                value: new DateTime(2025, 8, 8, 15, 16, 25, 855, DateTimeKind.Local).AddTicks(9396));

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
    }
}
