using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM.Migrations
{
    /// <inheritdoc />
    public partial class update_combo_available : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Combos",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "Combos",
                keyColumn: "ComboID",
                keyValue: 1,
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 10, 15, 3, 56, 139, DateTimeKind.Local).AddTicks(3104));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 10, 15, 3, 56, 139, DateTimeKind.Local).AddTicks(3112));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 10, 15, 3, 56, 139, DateTimeKind.Local).AddTicks(3118));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 10, 15, 3, 56, 678, DateTimeKind.Local).AddTicks(4339), "$2a$11$knPkkpHWzeMV8VtwRjJ9s.nEpV6R3BkyLoXCZG72Vls5RXXUStCnW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 10, 15, 3, 56, 413, DateTimeKind.Local).AddTicks(6645), "$2a$11$EsLO/tK2fXkvkmebhoeS.uam4U6.S8PaByqVDkV6VbD5mMIXmfdz6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Combos");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 10, 14, 9, 36, 174, DateTimeKind.Local).AddTicks(63));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 10, 14, 9, 36, 174, DateTimeKind.Local).AddTicks(69));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 10, 14, 9, 36, 174, DateTimeKind.Local).AddTicks(74));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 10, 14, 9, 36, 752, DateTimeKind.Local).AddTicks(7178), "$2a$11$KHIUV3Xt1i1y0hUHrd4QNuN2UUGY9U18Vm7VCopZvuf0MckJkuz1q" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 10, 14, 9, 36, 465, DateTimeKind.Local).AddTicks(1131), "$2a$11$mWB7YSyvAiV4SOvehP6hXuU98xhEDiosRQNRAuGCVJAo1WREJdqG6" });
        }
    }
}
