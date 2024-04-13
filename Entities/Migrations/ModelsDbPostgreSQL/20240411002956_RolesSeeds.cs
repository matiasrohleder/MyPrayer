using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations.ModelsDbPostgreSQL
{
    /// <inheritdoc />
    public partial class RolesSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0d6490bf-fdfd-4a68-99ab-c0bb135b28f7"), "ContentAdmin", "ContentAdmin", "CONTENTADMIN" },
                    { new Guid("8d02c502-afbb-4e9b-9552-6c2cabbd6864"), "CategoryAdmin", "CategoryAdmin", "CATEGORYADMIN" },
                    { new Guid("b8be6280-b3e8-48a3-9b01-7a361961595a"), "UserAdmin", "UserAdmin", "USERADMIN" },
                    { new Guid("f6c0b642-2ddb-42f5-8ba8-0ce9faeb3a4d"), "ReadingAdmin", "ReadingAdmin", "READINGADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10101010-3333-4b19-944f-eaaa655108fb"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAY/esc3I7UkMOZWzMgd+mYN6KMDrm6x6IY8bTSP+qg4gkwhauHytQ0BIsOuzEf1Mw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0d6490bf-fdfd-4a68-99ab-c0bb135b28f7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d02c502-afbb-4e9b-9552-6c2cabbd6864"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8be6280-b3e8-48a3-9b01-7a361961595a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f6c0b642-2ddb-42f5-8ba8-0ce9faeb3a4d"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10101010-3333-4b19-944f-eaaa655108fb"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEO2XGn21sz1CnicMZuf7QHjhKgq9GhW5/rm4WjtmOXeyyD78q74Nfl3K4GR17yEmTw==");
        }
    }
}
