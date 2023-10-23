using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSACalculation.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdentityData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "empId", "isAdmin" },
                values: new object[,]
                {
                    { "13f19533-ee72-4d51-b17e-86aa3b9ba734", 0, "6957876d-15ee-4a9d-9223-b1e5b020f347", "janedoe@test.com", false, "Jane", "Doe", false, null, null, "JANEDOE", "AQAAAAEAACcQAAAAEJ3o+jsBruBiExviH+CZUwlN8Ga3+qhqP+6vC3zjLYRaaidttRG/LIM1wNNmquWZtw==", null, false, "c1140929-f076-4a6a-a0d5-3aacf7d158c6", false, "janeDoe", 3, 0 },
                    { "bd03753f-9e95-4909-945d-abfb922bd7f8", 0, "caa748e3-9efc-4ab7-b7a7-c7297aba5a81", "johndoe@test.com", false, "John", "Doe", false, null, null, "JOHNDOE", "AQAAAAEAACcQAAAAEPbht3pain7bY4ApbcRQ8WmHQOJq6GOs3RlrrjLB76E6g5NmrPZXkDRopcZ1PNHr7Q==", null, false, "633a8e5d-59ac-4182-bac8-3410eaa8edbf", false, "johnDoe", 1, 0 },
                    { "f4340cf5-428a-4698-981e-31056ddbf063", 0, "aade9f4e-bf80-4470-a0a5-8777fff2b154", "admin@test.com", false, "Admin", "Admin", false, null, null, "ADMIN", "AQAAAAEAACcQAAAAECTiWMl+NzQkn/kw7pZyaldYMu4bR6mL6nPVpftfwaXycZG9rLlE+UdRmcFEYqjIGQ==", null, false, "4f9af955-744b-4563-a17c-d8791d70fa69", false, "admin", 2, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateSubmitted",
                value: new DateTime(2023, 10, 19, 10, 48, 1, 29, DateTimeKind.Utc).AddTicks(895));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "13f19533-ee72-4d51-b17e-86aa3b9ba734");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd03753f-9e95-4909-945d-abfb922bd7f8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f4340cf5-428a-4698-981e-31056ddbf063");

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
        }
    }
}
