using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisMarcadores.Data.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NombreUsuario = table.Column<string>(nullable: true),
                    IdEncuentro = table.Column<Guid>(nullable: false),
                    Texto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deportes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deportes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NombreUsuario = table.Column<string>(nullable: true),
                    IdEquipo = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sesiones",
                columns: table => new
                {
                    NombreUsuario = table.Column<string>(nullable: false),
                    Token = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesiones", x => x.NombreUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NombreUsuario = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Contraseña = table.Column<string>(nullable: true),
                    Administrador = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Foto = table.Column<string>(nullable: true),
                    DeporteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipos_Deportes_DeporteId",
                        column: x => x.DeporteId,
                        principalTable: "Deportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Encuentros",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FechaHora = table.Column<DateTime>(nullable: false),
                    DeporteId = table.Column<Guid>(nullable: true),
                    EquipoLocalId = table.Column<Guid>(nullable: true),
                    EquipoVisitanteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuentros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encuentros_Deportes_DeporteId",
                        column: x => x.DeporteId,
                        principalTable: "Deportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Encuentros_Equipos_EquipoLocalId",
                        column: x => x.EquipoLocalId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Encuentros_Equipos_EquipoVisitanteId",
                        column: x => x.EquipoVisitanteId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Encuentros_DeporteId",
                table: "Encuentros",
                column: "DeporteId");

            migrationBuilder.CreateIndex(
                name: "IX_Encuentros_EquipoLocalId",
                table: "Encuentros",
                column: "EquipoLocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Encuentros_EquipoVisitanteId",
                table: "Encuentros",
                column: "EquipoVisitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_DeporteId",
                table: "Equipos",
                column: "DeporteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Encuentros");

            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "Sesiones");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "Deportes");
        }
    }
}
