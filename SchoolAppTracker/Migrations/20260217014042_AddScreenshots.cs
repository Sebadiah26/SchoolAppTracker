using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAppTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddScreenshots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Screenshots",
                schema: "school",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenshots", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Screenshots_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "school",
                        principalTable: "Applications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Screenshots_ApplicationId",
                schema: "school",
                table: "Screenshots",
                column: "ApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Screenshots",
                schema: "school");
        }
    }
}
