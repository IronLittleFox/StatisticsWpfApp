using Microsoft.EntityFrameworkCore.Migrations;

namespace StatisticsDatabaseLibrary.Migrations
{
    public partial class PhotoAddedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScannedPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CopyBookId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScannedPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScannedPhotos_CopyBooks_CopyBookId",
                        column: x => x.CopyBookId,
                        principalTable: "CopyBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScannedPhotos_CopyBookId",
                table: "ScannedPhotos",
                column: "CopyBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScannedPhotos");
        }
    }
}
