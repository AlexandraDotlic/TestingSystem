using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Migrations
{
    public partial class Added_StudentTest_manyToMany_Relation_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTestQuestions_Students_StudentId",
                table: "StudentTestQuestions");

            migrationBuilder.DropIndex(
                name: "IX_StudentTestQuestions_StudentId",
                table: "StudentTestQuestions");

            migrationBuilder.AddColumn<int>(
                name: "StudentTestStudentId",
                table: "StudentTestQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "StudentTestTestId",
                table: "StudentTestQuestions",
                type: "smallint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentTests",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<short>(type: "smallint", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTests", x => new { x.StudentId, x.TestId });
                    table.ForeignKey(
                        name: "FK_StudentTests_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTests_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTestQuestions_StudentTestStudentId_StudentTestTestId",
                table: "StudentTestQuestions",
                columns: new[] { "StudentTestStudentId", "StudentTestTestId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTests_TestId",
                table: "StudentTests",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTestQuestions_StudentTests_StudentTestStudentId_StudentTestTestId",
                table: "StudentTestQuestions",
                columns: new[] { "StudentTestStudentId", "StudentTestTestId" },
                principalTable: "StudentTests",
                principalColumns: new[] { "StudentId", "TestId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTestQuestions_StudentTests_StudentTestStudentId_StudentTestTestId",
                table: "StudentTestQuestions");

            migrationBuilder.DropTable(
                name: "StudentTests");

            migrationBuilder.DropIndex(
                name: "IX_StudentTestQuestions_StudentTestStudentId_StudentTestTestId",
                table: "StudentTestQuestions");

            migrationBuilder.DropColumn(
                name: "StudentTestStudentId",
                table: "StudentTestQuestions");

            migrationBuilder.DropColumn(
                name: "StudentTestTestId",
                table: "StudentTestQuestions");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTestQuestions_StudentId",
                table: "StudentTestQuestions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTestQuestions_Students_StudentId",
                table: "StudentTestQuestions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
