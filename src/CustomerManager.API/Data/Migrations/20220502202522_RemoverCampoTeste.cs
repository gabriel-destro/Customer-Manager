using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerManager.Data.Migrations
{
    public partial class RemoverCampoTeste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teste",
                table: "Clientes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Teste",
                table: "Clientes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
