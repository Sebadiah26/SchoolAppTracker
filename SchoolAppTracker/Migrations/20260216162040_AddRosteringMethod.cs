using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAppTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddRosteringMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RosteringMethod",
                schema: "school",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RosteringMethod",
                schema: "school",
                table: "Applications");
        }
    }
}
