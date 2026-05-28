using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class migration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlayTables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "PlayTables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "PlayTables",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.AddColumn<string>(
                name: "TableCode",
                table: "PlayTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableCode",
                table: "PlayTables");

            migrationBuilder.InsertData(
                table: "PlayTables",
                columns: new[] { "Id", "CreatedAt", "LastVisitAt", "LocationStoreName", "ModifiedAt", "TableDescription", "TableName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "North", new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corner seating", "Corner Table" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Center", new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Center of the room", "Center Table" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "South", new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "VIP seating", "VIP Table" }
                });
        }
    }
}
