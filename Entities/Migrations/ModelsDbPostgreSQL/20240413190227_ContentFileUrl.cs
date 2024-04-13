using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations.ModelsDbPostgreSQL
{
    /// <inheritdoc />
    public partial class ContentFileUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "Contents",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10101010-3333-4b19-944f-eaaa655108fb"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFLeegm6H1aC+PJ8KJ5nPbc9vwkEOJKdI96KCyfeQnBa0lpFTctPiu/PXXXOCxNJ6g==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "Contents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10101010-3333-4b19-944f-eaaa655108fb"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAY/esc3I7UkMOZWzMgd+mYN6KMDrm6x6IY8bTSP+qg4gkwhauHytQ0BIsOuzEf1Mw==");
        }
    }
}
