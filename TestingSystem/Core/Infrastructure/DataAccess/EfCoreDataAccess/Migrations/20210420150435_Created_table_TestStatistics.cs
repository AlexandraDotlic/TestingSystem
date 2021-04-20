using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.DataAccess.EfCoreDataAccess.Migrations
{
    public partial class Created_table_TestStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTestPassed",
                table: "StudentTests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TestStatistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<short>(type: "smallint", nullable: false),
                    TestTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExaminerId = table.Column<int>(type: "int", nullable: false),
                    PercentageOfStudentsWhoPassedTheTest = table.Column<int>(type: "int", nullable: false),
                    NumberOfStudentsWhoTookTheTest = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStatistics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestStatistics");

            migrationBuilder.DropColumn(
                name: "IsTestPassed",
                table: "StudentTests");
        }
    }
}
