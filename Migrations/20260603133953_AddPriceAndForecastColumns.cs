using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioManagementMvc.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceAndForecastColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                table: "Assets",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Forecast2028",
                table: "Assets",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 1,
                columns: new[] { "CurrentPrice", "Forecast2028" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 2,
                columns: new[] { "CurrentPrice", "Forecast2028" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "AssetId",
                keyValue: 3,
                columns: new[] { "CurrentPrice", "Forecast2028" },
                values: new object[] { 0m, 0m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Forecast2028",
                table: "Assets");
        }
    }
}
