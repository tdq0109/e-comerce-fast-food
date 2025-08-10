using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM.Migrations
{
    /// <inheritdoc />
    public partial class AddComboToCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductID",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ComboID",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 9, 0, 34, 18, 133, DateTimeKind.Local).AddTicks(9376));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 9, 0, 34, 18, 133, DateTimeKind.Local).AddTicks(9381));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 9, 0, 34, 18, 133, DateTimeKind.Local).AddTicks(9386));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 9, 0, 34, 18, 683, DateTimeKind.Local).AddTicks(944), "$2a$11$RoH/mFMRjaEPbExQOmJ8NuBx5cg8t2ZsEEbsqYByYcl.3E6Hh6Xz6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 9, 0, 34, 18, 404, DateTimeKind.Local).AddTicks(6922), "$2a$11$ofU4asvTiO.stsx8dFNto.HI4Bm6O5oL.Chl1KozHPNdxvkXfZMry" });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ComboID",
                table: "Carts",
                column: "ComboID");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Combos_ComboID",
                table: "Carts",
                column: "ComboID",
                principalTable: "Combos",
                principalColumn: "ComboID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductID",
                table: "Carts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Combos_ComboID",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductID",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ComboID",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ComboID",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductID",
                table: "Carts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
