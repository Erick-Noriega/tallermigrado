using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tallermigrado.Migrations
{
    /// <inheritdoc />
    public partial class ini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pagosEmpleadoId",
                table: "produempleado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_produempleado_pagosEmpleadoId",
                table: "produempleado",
                column: "pagosEmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_produempleado_pagosempleado_pagosEmpleadoId",
                table: "produempleado",
                column: "pagosEmpleadoId",
                principalTable: "pagosempleado",
                principalColumn: "Id");
        }

    
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produempleado_pagosempleado_pagosEmpleadoId",
                table: "produempleado");

            migrationBuilder.DropIndex(
                name: "IX_produempleado_pagosEmpleadoId",
                table: "produempleado");

            migrationBuilder.DropColumn(
                name: "pagosEmpleadoId",
                table: "produempleado");
        }
    }
}
