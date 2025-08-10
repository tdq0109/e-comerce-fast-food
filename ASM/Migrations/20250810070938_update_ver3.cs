using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM.Migrations
{
    /// <inheritdoc />
    public partial class update_ver3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 8, 10, 14, 9, 36, 174, DateTimeKind.Local).AddTicks(63), 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 8, 10, 14, 9, 36, 174, DateTimeKind.Local).AddTicks(69), 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 8, 10, 14, 9, 36, 174, DateTimeKind.Local).AddTicks(74), 100 });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 8, 10, 13, 54, 12, 390, DateTimeKind.Local).AddTicks(9751), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 8, 10, 13, 54, 12, 390, DateTimeKind.Local).AddTicks(9756), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2025, 8, 10, 13, 54, 12, 390, DateTimeKind.Local).AddTicks(9761), 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 10, 13, 54, 12, 925, DateTimeKind.Local).AddTicks(3333), "$2a$11$m4Pt64GpmLS8Lf0gRVCGQuqGjRHynHJ1REogn94Mzj1xmaPvd/a6W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 10, 13, 54, 12, 659, DateTimeKind.Local).AddTicks(4565), "$2a$11$YD1bErIpfJi9EsKYbtQ6u.Mp8rrywTWKLT/IFjtNP1ZYaI1gVYBm2" });
        }
    }
}
