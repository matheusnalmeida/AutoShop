using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Infra.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Cpf_TempId1 = table.Column<int>(type: "int", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefone_TempId1 = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Email_TempId1 = table.Column<int>(type: "int", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Senha_TempId1 = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.UniqueConstraint("AK_Usuario_Cpf_TempId1", x => x.Cpf_TempId1);
                    table.UniqueConstraint("AK_Usuario_Email_TempId1", x => x.Email_TempId1);
                    table.UniqueConstraint("AK_Usuario_Senha_TempId1", x => x.Senha_TempId1);
                    table.UniqueConstraint("AK_Usuario_Telefone_TempId1", x => x.Telefone_TempId1);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ImagemURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operacao",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ValorFinanciado = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ValorVeiculo = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    QuantidadeDeParcelas = table.Column<int>(type: "int", nullable: false),
                    IdVeiculo = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    IdCliente = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    IdVendedor = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operacao_Usuario_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Operacao_Usuario_IdVendedor",
                        column: x => x.IdVendedor,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Operacao_Veiculo_IdVeiculo",
                        column: x => x.IdVeiculo,
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoOperacao",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdOperacao = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    IdProduto = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoOperacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoOperacao_Operacao_IdOperacao",
                        column: x => x.IdOperacao,
                        principalTable: "Operacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoOperacao_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operacao_IdCliente",
                table: "Operacao",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Operacao_IdVeiculo",
                table: "Operacao",
                column: "IdVeiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Operacao_IdVendedor",
                table: "Operacao",
                column: "IdVendedor");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoOperacao_IdOperacao",
                table: "ProdutoOperacao",
                column: "IdOperacao");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoOperacao_IdProduto",
                table: "ProdutoOperacao",
                column: "IdProduto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoOperacao");

            migrationBuilder.DropTable(
                name: "Operacao");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Veiculo");
        }
    }
}
