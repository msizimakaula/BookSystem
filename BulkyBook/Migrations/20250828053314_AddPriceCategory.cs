using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBook.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Categories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Categories");
        }
    }
}
