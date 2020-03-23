using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AjudaSolidaria.Respository.Migrations
{
    public partial class InitialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pessoa",
                columns: table => new
                {
                    key = table.Column<Guid>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false, defaultValueSql: "current_timestamp"),
                    updated_at = table.Column<DateTime>(nullable: false, defaultValueSql: "current_timestamp"),
                    nome = table.Column<string>(maxLength: 100, nullable: true),
                    cpf = table.Column<string>(type: "bpchar(11)", nullable: true),
                    telefone = table.Column<string>(type: "bpchar(11)", nullable: true),
                    data_nascimento = table.Column<DateTime>(nullable: false),
                    estado = table.Column<string>(type: "bpchar(2)", nullable: true),
                    cidade = table.Column<string>(type: "text", nullable: true),
                    cep = table.Column<string>(nullable: true),
                    bairro = table.Column<string>(type: "text", nullable: true),
                    endereco = table.Column<string>(type: "text", nullable: true),
                    numero = table.Column<string>(type: "text", nullable: true),
                    coordenadas = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pessoa", x => x.key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pessoa");
        }
    }
}
