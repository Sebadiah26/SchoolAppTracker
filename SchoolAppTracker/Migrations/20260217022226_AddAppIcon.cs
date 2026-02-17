using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAppTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddAppIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconPath",
                schema: "school",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconPath",
                schema: "school",
                table: "Applications");
        }
    }
}
