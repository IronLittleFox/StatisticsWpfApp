using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StatisticMobileDatabaseLibrary.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegisteredUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CopyBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    BarCode = table.Column<string>(nullable: true),
                    PageFrom = table.Column<int>(nullable: true),
                    PageTo = table.Column<int>(nullable: true),
                    RegisteredUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopyBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CopyBooks_RegisteredUsers_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "RegisteredUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScannedPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Photo = table.Column<byte[]>(nullable: true),
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
                name: "IX_CopyBooks_RegisteredUserId",
                table: "CopyBooks",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScannedPhotos_CopyBookId",
                table: "ScannedPhotos",
                column: "CopyBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScannedPhotos");

            migrationBuilder.DropTable(
                name: "CopyBooks");

            migrationBuilder.DropTable(
                name: "RegisteredUsers");
        }
    }
}
