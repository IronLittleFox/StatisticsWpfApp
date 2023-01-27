using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StatisticsDatabaseLibrary.Migrations
{
    public partial class PhotoArrayAddedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "ScannedPhotos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "ScannedPhotos");
        }
    }
}
