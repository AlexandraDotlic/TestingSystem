using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Migrations
{
    public partial class Updated_StudentTestQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTestQuestions_Questions_QuestionId",
                table: "StudentTestQuestions");

            migrationBuilder.DropIndex(
                name: "IX_StudentTestQuestions_QuestionId",
                table: "StudentTestQuestions");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentTestQuestions");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "StudentTestQuestions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "StudentTestQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "TestId",
                table: "StudentTestQuestions",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentTestQuestions_QuestionId",
                table: "StudentTestQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTestQuestions_Questions_QuestionId",
                table: "StudentTestQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
