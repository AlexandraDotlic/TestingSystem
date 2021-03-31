using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Migrations
{
    public partial class Renamed_AccountId_to_ExternalId_in_Student_and_Examiner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Examiners");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Examiners",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Examiners");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Examiners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
