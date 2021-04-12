using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Migrations
{
    public partial class Updated_AnswerOption_table_removed_Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerOptions",
                table: "AnswerOptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AnswerOptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Id",
                table: "AnswerOptions",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerOptions",
                table: "AnswerOptions",
                column: "Id");
        }
    }
}
