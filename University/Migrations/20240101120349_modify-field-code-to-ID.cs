using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Migrations
{
    public partial class modifyfieldcodetoID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Lessons",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Lessons",
                newName: "Code");
        }
    }
}
