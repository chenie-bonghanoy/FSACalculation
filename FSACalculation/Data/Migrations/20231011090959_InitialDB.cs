using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSACalculation.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FSAAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoverageYear = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClaimAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalClaimAmount = table.Column<double>(type: "float", nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Claims_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CoverageYear", "FSAAmount", "FirstName", "LastName" },
                values: new object[] { 1, "2023", 5000m, "John", "Doe" });

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "ID", "ClaimAmount", "DateSubmitted", "EmployeeId", "ReceiptAmount", "ReceiptDate", "ReceiptNo", "ReferenceNo", "Status", "TotalClaimAmount" },
                values: new object[] { 1, 1000m, new DateTime(2023, 10, 11, 9, 9, 59, 55, DateTimeKind.Utc).AddTicks(9113), 1, 1000m, new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "41680764", "123456", 0, 1000.0 });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_EmployeeId",
                table: "Claims",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
