using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoboostTask.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Stocks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Stocks");
        }
    }
}
