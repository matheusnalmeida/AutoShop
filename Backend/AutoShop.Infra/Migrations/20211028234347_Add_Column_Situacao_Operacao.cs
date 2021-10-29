using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoShop.Infra.Migrations
{
    public partial class Add_Column_Situacao_Operacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Operacao",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Operacao");
        }
    }
}
