using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stock.management.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class StockManagementDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Priceperunit",
                table: "ProductDetail",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Priceperunit",
                table: "ProductDetail",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
