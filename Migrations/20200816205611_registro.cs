using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Marlin_Desafio_Backend_Junior.Migrations
{
    public partial class registro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    Staus = table.Column<string>(nullable: false),
                    Matricula = table.Column<string>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registros");
        }
    }
}
