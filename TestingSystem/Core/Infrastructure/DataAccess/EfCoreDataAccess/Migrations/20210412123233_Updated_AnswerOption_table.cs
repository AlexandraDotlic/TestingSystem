using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Migrations
{
    public partial class Updated_AnswerOption_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AnswerOptions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerOptions",
                table: "AnswerOptions",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerOptions",
                table: "AnswerOptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AnswerOptions");
        }
    }
}
