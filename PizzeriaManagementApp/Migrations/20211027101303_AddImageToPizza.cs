using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzeriaManagementApp.Migrations
{
    public partial class AddImageToPizza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Pizzas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Pizzas");
        }
    }
}
