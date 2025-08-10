using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ASM.Migrations
{
    /// <inheritdoc />
    public partial class ver3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Burger" },
                    { 2, "Pizza" },
                    { 3, "Nước uống" }
                });

            migrationBuilder.InsertData(
                table: "Combos",
                columns: new[] { "ComboID", "ComboName", "Description", "ImageUrl", "Price" },
                values: new object[] { 1, "Combo Burger + Nước", "Gồm 1 burger bò và 1 lon Coca", "/images/combo1.jpg", 65000m });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "AvatarUrl", "CreatedAt", "DateOfBirth", "Email", "FullName", "Gender", "GoogleId", "IsActive", "PasswordHash", "Phone", "Role" },
                values: new object[,]
                {
                    { -2, "456 Second St", null, new DateTime(2025, 7, 27, 11, 8, 33, 711, DateTimeKind.Local).AddTicks(3771), null, "a@gmail.com", "Nguyễn Văn A", null, null, true, "hashedPassword", "0912345678", "Customer" },
                    { -1, "123 Main St", null, new DateTime(2025, 7, 27, 11, 8, 33, 711, DateTimeKind.Local).AddTicks(3766), null, "admin@example.com", "Admin", null, null, true, "hashedPassword", "0909999999", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "CreatedAt", "Description", "ImageURL", "IsAvailable", "Price", "ProductName", "Tags", "Topic" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 7, 27, 11, 8, 33, 710, DateTimeKind.Local).AddTicks(8121), "Burger thịt bò, phô mai và rau", "/images/burger.jpg", true, 55000m, "Burger Bò Phô Mai", "burger,bo,pho mai", "Best Seller" },
                    { 2, 2, new DateTime(2025, 7, 27, 11, 8, 33, 710, DateTimeKind.Local).AddTicks(8126), "Pizza topping hải sản và phô mai", "/images/pizza.jpg", true, 120000m, "Pizza Hải Sản", "pizza,hai san", "Mới" },
                    { 3, 3, new DateTime(2025, 7, 27, 11, 8, 33, 710, DateTimeKind.Local).AddTicks(8130), "Thức uống có gas", "/images/coca.jpg", true, 15000m, "Coca-Cola Lon", null, null }
                });

            migrationBuilder.InsertData(
                table: "ComboItems",
                columns: new[] { "ComboID", "ProductID", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 1, 3, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ComboItems",
                keyColumns: new[] { "ComboID", "ProductID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ComboItems",
                keyColumns: new[] { "ComboID", "ProductID" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Combos",
                keyColumn: "ComboID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);
        }
    }
}
