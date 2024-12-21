using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WEB_Project.Migrations
{
    /// <inheritdoc />
    public partial class salon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Salons",
                columns: new[] { "Id", "Address", "Name", "PhoneNumber", "WorkingHours" },
                values: new object[] { 1, "123 Salon Street", "My Salon", "123-456-7890", "09.00- 18.00" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Salons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Num" },
                values: new object[,]
                {
                    { 1, "Saç Kesimi", 0 },
                    { 2, "Saç Boyama", 0 },
                    { 3, "Yüz Bakımı", 0 }
                });
        }
    }
}
