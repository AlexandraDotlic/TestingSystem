using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Migrations
{
    public partial class Added_AccountId_to_Student_and_Examiner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Examiners");
        }
    }
}
