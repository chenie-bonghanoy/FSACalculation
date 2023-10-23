using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSACalculation.Migrations
{
    /// <inheritdoc />
    public partial class NewUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "49e4e437-c8da-48fe-906b-1e8519e5f5b1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e5ca1919-b75b-4e88-9c51-64e6ae1cda0e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "empId", "isAdmin" },
                values: new object[,]
                {
                    { "302f2311-110d-43d5-bfbf-143e1737cf8a", 0, "ac26d003-8c75-4033-8d27-d095c4247e1b", null, false, "Jane", "Doe", false, null, null, "JANEDOE", "AQAAAAEAACcQAAAAENjt3rJg3cdJuF2sIAEX4UKoGXGpXt97W6PiRvtWEeTzfMyoaav43ym00FRL861htA==", null, false, "20a068f1-6cd9-4850-9065-075a3603bde1", false, "janeDoe", 3, 0 },
                    { "6e7d76d6-bb00-4c5c-8aea-911d085f8fe7", 0, "72de4e01-2ded-49b4-87cc-6cf5c07d13e7", null, false, "John", "Doe", false, null, null, "JOHNDOE", "AQAAAAEAACcQAAAAEPGtgR6nE2lNKWu1KPwN0WjX4uRiEWD6j1bvMiV4NXbBv2co6aQrL8y7G443C7znYQ==", null, false, "7c138cd4-b2eb-4090-9d3a-e67d9b1a88f0", false, "johnDoe", 1, 0 },
                    { "c816e504-fd6e-4c52-97c3-91cbeb386cb5", 0, "8e6146e8-15ec-43dd-898e-4a8f150079a9", null, false, "Admin", "Admin", false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEDLmfgM747Z4ngjwjE+E9ZnOg2os6XNT8PgX7FygQ2R9XA7xUFaZwHscJ8GYtI420w==", null, false, "ea02828f-9a54-4f70-90be-65b3de4c7ca5", false, "admin", 2, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateSubmitted",
                value: new DateTime(2023, 10, 17, 9, 39, 26, 55, DateTimeKind.Utc).AddTicks(9744));

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CoverageYear", "FSAAmount", "FirstName", "LastName" },
                values: new object[] { 3, "2023", 7000m, "Jane", "Doe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "302f2311-110d-43d5-bfbf-143e1737cf8a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6e7d76d6-bb00-4c5c-8aea-911d085f8fe7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c816e504-fd6e-4c52-97c3-91cbeb386cb5");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

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
                column: "DateSubmitted",
                value: new DateTime(2023, 10, 16, 9, 40, 42, 962, DateTimeKind.Utc).AddTicks(9329));
        }
    }
}
