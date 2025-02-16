using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartInventoryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddGuestInfoToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Apparel and accessories", "Clothing" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "LowStockThreshold", "Name", "Price", "Quantity" },
                values: new object[] { 3, 3, "Cotton T-shirt", 5, "T-Shirt", 20m, 50 });
        }
    }
}
