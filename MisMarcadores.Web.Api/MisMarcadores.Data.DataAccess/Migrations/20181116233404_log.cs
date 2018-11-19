using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisMarcadores.Data.DataAccess.Migrations
{
    public partial class log : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Usuario = table.Column<string>(nullable: true),
                    Accion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");
        }
    }
}
