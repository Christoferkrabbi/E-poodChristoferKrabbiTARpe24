using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class migration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayTables",
                table: "PlayTables");

            migrationBuilder.DeleteData(
                table: "PlayTables",
                keyColumn: "TableID",
                keyColumnType: "nvarchar(450)",
                keyValue: "T1");

            migrationBuilder.DeleteData(
                table: "PlayTables",
                keyColumn: "TableID",
                keyColumnType: "nvarchar(450)",
                keyValue: "T2");

            migrationBuilder.DeleteData(
                table: "PlayTables",
                keyColumn: "TableID",
                keyColumnType: "nvarchar(450)",
                keyValue: "T3");

            migrationBuilder.DropColumn(
                name: "TableID",
                table: "PlayTables");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "PlayTables");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "PlayTables");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PlayTables",
                newName: "TableName");

            migrationBuilder.RenameColumn(
                name: "TableID",
                table: "PlayTableBookings",
                newName: "PlayTableID");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PlayTables",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PlayTables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVisitAt",
                table: "PlayTables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LocationStoreName",
                table: "PlayTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "PlayTables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TableDescription",
                table: "PlayTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayTables",
                table: "PlayTables",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayTables",
                table: "PlayTables");

            migrationBuilder.DeleteData(
                table: "PlayTables",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "PlayTables",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "PlayTables",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PlayTables");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PlayTables");

            migrationBuilder.DropColumn(
                name: "LastVisitAt",
                table: "PlayTables");

            migrationBuilder.DropColumn(
                name: "LocationStoreName",
                table: "PlayTables");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "PlayTables");

            migrationBuilder.DropColumn(
                name: "TableDescription",
                table: "PlayTables");

            migrationBuilder.RenameColumn(
                name: "TableName",
                table: "PlayTables",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PlayTableID",
                table: "PlayTableBookings",
                newName: "TableID");

            migrationBuilder.AddColumn<string>(
                name: "TableID",
                table: "PlayTables",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "PlayTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "PlayTables",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayTables",
                table: "PlayTables",
                column: "TableID");

            migrationBuilder.InsertData(
                table: "PlayTables",
                columns: new[] { "TableID", "Capacity", "Location", "Name" },
                values: new object[,]
                {
                    { "T1", 4, "North", "Corner Table" },
                    { "T2", 6, "Center", "Center Table" },
                    { "T3", 2, "South", "VIP Table" }
                });
        }
    }
}
