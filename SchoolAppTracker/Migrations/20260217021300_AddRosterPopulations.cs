using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAppTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddRosterPopulations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RosterGuardians",
                schema: "school",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RosterStudents",
                schema: "school",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RosterTeachers",
                schema: "school",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RosterGuardians",
                schema: "school",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "RosterStudents",
                schema: "school",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "RosterTeachers",
                schema: "school",
                table: "Applications");
        }
    }
}
