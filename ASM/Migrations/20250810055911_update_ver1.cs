using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM.Migrations
{
    /// <inheritdoc />
    public partial class update_ver1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Combos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Combos",
                keyColumn: "ComboID",
                keyValue: 1,
                column: "Quantity",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 8, 10, 12, 59, 6, 207, DateTimeKind.Local).AddTicks(4848), 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 8, 10, 12, 59, 6, 207, DateTimeKind.Local).AddTicks(4853), 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 8, 10, 12, 59, 6, 207, DateTimeKind.Local).AddTicks(4858), 100 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 10, 12, 59, 6, 754, DateTimeKind.Local).AddTicks(9162), "$2a$11$Kw5abenvp7ca1SxbK0XZoewDYTc1gcIMznI.kHXJ2RpfzAmB4lU32" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 10, 12, 59, 6, 476, DateTimeKind.Local).AddTicks(593), "$2a$11$e5onmnJna/IJeUcjTn5GtuMS0dsBVIp3glJ2ILF3sD0YCB/bckDN6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Combos");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 9, 2, 13, 19, 448, DateTimeKind.Local).AddTicks(6013));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 9, 2, 13, 19, 448, DateTimeKind.Local).AddTicks(6018));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 9, 2, 13, 19, 448, DateTimeKind.Local).AddTicks(6022));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 9, 2, 13, 19, 984, DateTimeKind.Local).AddTicks(2690), "$2a$11$fIjXaSG9pEtFS.qPDMzB4uWqZ7klwy9C4STGgshW8Y5N3t.MK6JnK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 9, 2, 13, 19, 718, DateTimeKind.Local).AddTicks(2953), "$2a$11$.YFqSRVnOSmhyW.icI/wbe1m3lszurktjYLRe7rPgHcv9CgH8SalG" });
        }
    }
}
