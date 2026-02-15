using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolAppTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "school");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "school",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "school",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GradeLevels",
                schema: "school",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeLevels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                schema: "school",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    SsoEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SsoProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FerpaCompliant = table.Column<bool>(type: "bit", nullable: false),
                    CoppaCompliant = table.Column<bool>(type: "bit", nullable: false),
                    DataPrivacyAgreementUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnnualCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LicenseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfLicenses = table.Column<int>(type: "int", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Applications_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "school",
                        principalTable: "Categories",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationDepartments",
                schema: "school",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDepartments", x => new { x.ApplicationId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_ApplicationDepartments_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "school",
                        principalTable: "Applications",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ApplicationDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "school",
                        principalTable: "Departments",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationGradeLevels",
                schema: "school",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    GradeLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationGradeLevels", x => new { x.ApplicationId, x.GradeLevelId });
                    table.ForeignKey(
                        name: "FK_ApplicationGradeLevels_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "school",
                        principalTable: "Applications",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ApplicationGradeLevels_GradeLevels_GradeLevelId",
                        column: x => x.GradeLevelId,
                        principalSchema: "school",
                        principalTable: "GradeLevels",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                schema: "school",
                table: "Categories",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Learning Management" },
                    { 2, "Assessment" },
                    { 3, "Communication" },
                    { 4, "Productivity" },
                    { 5, "Administrative" },
                    { 6, "Special Education" }
                });

            migrationBuilder.InsertData(
                schema: "school",
                table: "Departments",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Math" },
                    { 2, "Science" },
                    { 3, "English" },
                    { 4, "Social Studies" },
                    { 5, "IT" },
                    { 6, "Administration" },
                    { 7, "Counseling" },
                    { 8, "Special Education" }
                });

            migrationBuilder.InsertData(
                schema: "school",
                table: "GradeLevels",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "K" },
                    { 2, "1" },
                    { 3, "2" },
                    { 4, "3" },
                    { 5, "4" },
                    { 6, "5" },
                    { 7, "6" },
                    { 8, "7" },
                    { 9, "8" },
                    { 10, "9" },
                    { 11, "10" },
                    { 12, "11" },
                    { 13, "12" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDepartments_DepartmentId",
                schema: "school",
                table: "ApplicationDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationGradeLevels_GradeLevelId",
                schema: "school",
                table: "ApplicationGradeLevels",
                column: "GradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CategoryId",
                schema: "school",
                table: "Applications",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationDepartments",
                schema: "school");

            migrationBuilder.DropTable(
                name: "ApplicationGradeLevels",
                schema: "school");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "school");

            migrationBuilder.DropTable(
                name: "Applications",
                schema: "school");

            migrationBuilder.DropTable(
                name: "GradeLevels",
                schema: "school");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "school");
        }
    }
}
