using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations.ModelsDbPostgreSQL
{
    /// <inheritdoc />
    public partial class AddQuotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyQuotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastEditorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEditedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyQuotes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("166800f3-0cb9-4b19-9465-d63dedd2608b"), "DailyQuoteAdmin", "DailyQuoteAdmin", "DAILYQUOTEADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10101010-3333-4b19-944f-eaaa655108fb"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGzLsV3sOuWiSu4t1dbp10nSOzzOniPerEKwzlwLf8K0+UN+4Z+uTjjaiciKndmzOA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyQuotes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("166800f3-0cb9-4b19-9465-d63dedd2608b"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("10101010-3333-4b19-944f-eaaa655108fb"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFLeegm6H1aC+PJ8KJ5nPbc9vwkEOJKdI96KCyfeQnBa0lpFTctPiu/PXXXOCxNJ6g==");
        }
    }
}
