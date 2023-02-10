using Microsoft.EntityFrameworkCore.Migrations;

namespace StatisticMobileDatabaseLibrary.Migrations
{
    public partial class AddGPSColumnMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "CopyBooks",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "CopyBooks",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "CopyBooks");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "CopyBooks");
        }
    }
}
