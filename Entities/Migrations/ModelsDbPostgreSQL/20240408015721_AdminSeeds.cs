using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations.ModelsDbPostgreSQL
{
    /// <inheritdoc />
    public partial class AdminSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("3f3dd5b3-b480-4ea2-a477-101010101010"), "Admin", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "CreatorId", "Deleted", "Email", "EmailConfirmed", "LastEditedDate", "LastEditorId", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("10101010-3333-4b19-944f-eaaa655108fb"), 0, "admin@myprayer.com", new DateTime(2022, 12, 17, 23, 30, 0, 0, DateTimeKind.Utc), new Guid("10101010-3333-4b19-944f-eaaa655108fb"), false, "admin@myprayer.com", true, new DateTime(2022, 12, 17, 23, 30, 0, 0, DateTimeKind.Utc), new Guid("10101010-3333-4b19-944f-eaaa655108fb"), "MyPrayer", false, null, "Admin", "ADMIN@MYPRAYER.COM", "ADMIN@MYPRAYER.COM", "AQAAAAIAAYagAAAAEO2XGn21sz1CnicMZuf7QHjhKgq9GhW5/rm4WjtmOXeyyD78q74Nfl3K4GR17yEmTw==", null, false, "3AA6005F-8E18-4A00-B9E2-C2539C60A8C1", false, "admin@myprayer.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("3f3dd5b3-b480-4ea2-a477-101010101010"), new Guid("10101010-3333-4b19-944f-eaaa655108fb") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3f3dd5b3-b480-4ea2-a477-101010101010"), new Guid("10101010-3333-4b19-944f-eaaa655108fb") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3f3dd5b3-b480-4ea2-a477-101010101010"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10101010-3333-4b19-944f-eaaa655108fb"));
        }
    }
}
