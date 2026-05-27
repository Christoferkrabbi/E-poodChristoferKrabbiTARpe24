using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class playtablessqltestmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayTables",
                columns: table => new
                {
                    TableID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayTables", x => x.TableID);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayTables");
        }
    }
}
