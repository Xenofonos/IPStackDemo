using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPStack.API.Migrations
{
    /// <inheritdoc />
    public partial class IPDetailsDBInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPDetailsSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ip = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Continent = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    IPDetailsEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPDetailsSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IPDetailsSet_IPDetailsSet_IPDetailsEntityId",
                        column: x => x.IPDetailsEntityId,
                        principalTable: "IPDetailsSet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPDetailsSet_IPDetailsEntityId",
                table: "IPDetailsSet",
                column: "IPDetailsEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPDetailsSet");
        }
    }
}
