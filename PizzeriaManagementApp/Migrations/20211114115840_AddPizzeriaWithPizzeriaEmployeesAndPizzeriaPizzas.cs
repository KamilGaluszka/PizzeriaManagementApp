using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzeriaManagementApp.Migrations
{
    public partial class AddPizzeriaWithPizzeriaEmployeesAndPizzeriaPizzas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pizzerias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzerias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pizzerias_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PizzeriaEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PizzeriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzeriaEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzeriaEmployees_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PizzeriaEmployees_Pizzerias_PizzeriaId",
                        column: x => x.PizzeriaId,
                        principalTable: "Pizzerias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PizzeriaPizzas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PizzeriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PizzaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzeriaPizzas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzeriaPizzas_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzeriaPizzas_Pizzerias_PizzeriaId",
                        column: x => x.PizzeriaId,
                        principalTable: "Pizzerias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzeriaEmployees_EmployeeId",
                table: "PizzeriaEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzeriaEmployees_PizzeriaId",
                table: "PizzeriaEmployees",
                column: "PizzeriaId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzeriaPizzas_PizzaId",
                table: "PizzeriaPizzas",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzeriaPizzas_PizzeriaId",
                table: "PizzeriaPizzas",
                column: "PizzeriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pizzerias_AddressId",
                table: "Pizzerias",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzeriaEmployees");

            migrationBuilder.DropTable(
                name: "PizzeriaPizzas");

            migrationBuilder.DropTable(
                name: "Pizzerias");
        }
    }
}
