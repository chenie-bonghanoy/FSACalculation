using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSACalculation.Migrations
{
    /// <inheritdoc />
    public partial class DBUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90d862b4-5759-4063-a250-78c46ca527ca");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bed2fc91-7820-4616-a813-7f1cb743c042");

            migrationBuilder.AlterColumn<decimal>(
                name: "FSAAmount",
                table: "Employees",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalClaimAmount",
                table: "Claims",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNo",
                table: "Claims",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNo",
                table: "Claims",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReceiptAmount",
                table: "Claims",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ClaimAmount",
                table: "Claims",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "empId", "isAdmin" },
                values: new object[,]
                {
                    { "49e4e437-c8da-48fe-906b-1e8519e5f5b1", 0, "e30e9663-33ce-47f2-8d81-30505e541e4e", null, false, "Admin", "Admin", false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEFY+LUHUu6cSaqrjdowy0KMB3wXi/TsJB4iXTPjgYElsjgl7vSxxqcwQSs84l7cjwA==", null, false, "65904565-c4fd-4205-800c-cfa6db040a59", false, "admin", 2, 1 },
                    { "e5ca1919-b75b-4e88-9c51-64e6ae1cda0e", 0, "ab8c92d6-8587-4648-ad99-ad9280c55850", null, false, "John", "Doe", false, null, null, "JOHNDOE", "AQAAAAEAACcQAAAAEIo4xSG6N3NW807PAjUVhsa9t9v4KszaxAzakxuZkVNwSChNLmSe30t4nFE/PjuegQ==", null, false, "0127993a-2f64-43d5-ab9b-13c01bf69d9d", false, "johnDoe", 1, 0 }
                });

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "DateSubmitted", "TotalClaimAmount" },
                values: new object[] { new DateTime(2023, 10, 16, 9, 40, 42, 962, DateTimeKind.Utc).AddTicks(9329), 1000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "49e4e437-c8da-48fe-906b-1e8519e5f5b1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e5ca1919-b75b-4e88-9c51-64e6ae1cda0e");

            migrationBuilder.AlterColumn<decimal>(
                name: "FSAAmount",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<double>(
                name: "TotalClaimAmount",
                table: "Claims",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNo",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNo",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReceiptAmount",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ClaimAmount",
                table: "Claims",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "empId", "isAdmin" },
                values: new object[,]
                {
                    { "90d862b4-5759-4063-a250-78c46ca527ca", 0, "7ec15cd3-fa4e-402a-a353-5b2393bdf271", null, false, "Admin", "Admin", false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEI9AHKf2+Z6MNLt9GDxv7sPBEIh1RvsSNy6pK8Ewg4s6WfUQWg5ZXLdneyTHecfYfg==", null, false, "bc0b1578-6e6d-4efe-bbab-29f11369406a", false, "admin", 2, 1 },
                    { "bed2fc91-7820-4616-a813-7f1cb743c042", 0, "6c630932-57fb-4c4b-b3ab-9090f3309c0b", null, false, "John", "Doe", false, null, null, "JOHNDOE", "AQAAAAEAACcQAAAAENJ/1/PjYe9GfWT2fH8WEVReJ49+zBzXPq0K30UFaVNLk8TYWsvi1AB7pZtzEJZAfQ==", null, false, "eb8d356b-71b6-41b9-b2a4-73a5a5e25610", false, "johnDoe", 1, 0 }
                });

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "DateSubmitted", "TotalClaimAmount" },
                values: new object[] { new DateTime(2023, 10, 13, 13, 32, 56, 916, DateTimeKind.Utc).AddTicks(3424), 1000.0 });
        }
    }
}
