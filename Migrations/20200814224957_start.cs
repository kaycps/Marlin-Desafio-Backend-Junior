using Microsoft.EntityFrameworkCore.Migrations;

namespace Marlin_Desafio_Backend_Junior.Migrations
{
    public partial class start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    
                });

            
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(maxLength: 25, nullable: false),
                    Senha = table.Column<string>(maxLength: 25, nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
            

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<string>(maxLength: 45, nullable: false),
                    Nome = table.Column<string>(maxLength: 45, nullable: false),
                    idTurma = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Turmas_idTurma",
                        column: x => x.idTurma,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_Matricula",
                table: "Alunos",
                column: "Matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_Nome",
                table: "Alunos",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_idTurma",
                table: "Alunos",
                column: "idTurma");

            //populando usuario padrao para login
            migrationBuilder.Sql("INSERT INTO Usuarios(Login,Senha,Role) VALUES('marlin','marlin','adm')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Turmas");
        }
    }
}
