using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AjudaSolidaria.Respository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cidade",
                columns: table => new
                {
                    id = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(maxLength: 100, nullable: true),
                    codigo_ibge = table.Column<long>(nullable: false),
                    uf = table.Column<string>(type: "bpchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cidade", x => x.id);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_cidade_uf",
                table: "cidade",
                column: "uf");

            migrationBuilder.CreateIndex(
                name: "ix_cidade_uf_nome",
                table: "cidade",
                columns: new[] { "uf", "nome" });

            migrationBuilder.CreateIndex(
                name: "ix_pessoa_cpf",
                table: "pessoa",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_pessoa_cpf_data_nascimento",
                table: "pessoa",
                columns: new[] { "cpf", "data_nascimento" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cidade");

            migrationBuilder.DropTable(
                name: "pessoa");
        }
    }
}
