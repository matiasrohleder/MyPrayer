using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations.ModelsDbPostgreSQL
{
    /// <inheritdoc />
    public partial class ContentDateEnd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShowDate",
                table: "Contents",
                newName: "DateStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "Contents",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10101010-3333-4b19-944f-eaaa655108fb"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEF2kOT8DMc18RinKsAnsj0RdoYwMrQAfEYLNWpZv/Ega1Zk0fceAtq+UlEzyKQeYEg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "Contents");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "Contents",
                newName: "ShowDate");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10101010-3333-4b19-944f-eaaa655108fb"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFzOWTezwCnIwG0W/01giVpViGs1u8wYFE7R3nhYIhz1cTX1P7zyf282hZMCGuwVgg==");
        }
    }
}
