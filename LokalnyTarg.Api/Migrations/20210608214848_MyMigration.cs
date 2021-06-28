using Microsoft.EntityFrameworkCore.Migrations;

namespace LokalnyTarg.Api.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "UsertId",
                table: "Supplier",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsertId",
                table: "Supplier");
        }
    }
}
