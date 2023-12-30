using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Migrations
{
    public partial class Alter_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonID",
                table: "JunctionThSts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JunctionThSts_LessonID",
                table: "JunctionThSts",
                column: "LessonID");

            migrationBuilder.AddForeignKey(
                name: "FK_JunctionThSts_Lessons_LessonID",
                table: "JunctionThSts",
                column: "LessonID",
                principalTable: "Lessons",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JunctionThSts_Lessons_LessonID",
                table: "JunctionThSts");

            migrationBuilder.DropIndex(
                name: "IX_JunctionThSts_LessonID",
                table: "JunctionThSts");

            migrationBuilder.DropColumn(
                name: "LessonID",
                table: "JunctionThSts");
        }
    }
}
