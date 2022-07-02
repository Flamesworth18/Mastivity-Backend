using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductivityAppAPI.Migrations
{
    public partial class addedtotaltable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Totals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCount = table.Column<int>(type: "int", nullable: false),
                    NoteCount = table.Column<int>(type: "int", nullable: false),
                    TodoCount = table.Column<int>(type: "int", nullable: false),
                    TaskCount = table.Column<int>(type: "int", nullable: false),
                    ProgramCount = table.Column<int>(type: "int", nullable: false),
                    SubjectCount = table.Column<int>(type: "int", nullable: false),
                    StudentCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Totals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Totals");
        }
    }
}
