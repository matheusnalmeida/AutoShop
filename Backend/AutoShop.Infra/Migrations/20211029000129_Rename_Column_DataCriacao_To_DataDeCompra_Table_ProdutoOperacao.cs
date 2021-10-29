using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Infra.Migrations
{
    public partial class Rename_Column_DataCriacao_To_DataDeCompra_Table_ProdutoOperacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "ProdutoOperacao",
                newName: "DataDeCompra");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataDeCompra",
                table: "ProdutoOperacao",
                newName: "DataCriacao");
        }
    }
}
