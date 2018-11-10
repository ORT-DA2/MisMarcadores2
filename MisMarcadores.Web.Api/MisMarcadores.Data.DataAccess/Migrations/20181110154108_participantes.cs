using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisMarcadores.Data.DataAccess.Migrations
{
    public partial class participantes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encuentros_Equipos_EquipoLocalId",
                table: "Encuentros");

            migrationBuilder.DropForeignKey(
                name: "FK_Encuentros_Equipos_EquipoVisitanteId",
                table: "Encuentros");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.RenameColumn(
                name: "IdEquipo",
                table: "Favoritos",
                newName: "IdParticipante");

            migrationBuilder.RenameColumn(
                name: "EquipoVisitanteId",
                table: "Encuentros",
                newName: "ParticipanteVisitanteId");

            migrationBuilder.RenameColumn(
                name: "EquipoLocalId",
                table: "Encuentros",
                newName: "ParticipanteLocalId");

            migrationBuilder.RenameIndex(
                name: "IX_Encuentros_EquipoVisitanteId",
                table: "Encuentros",
                newName: "IX_Encuentros_ParticipanteVisitanteId");

            migrationBuilder.RenameIndex(
                name: "IX_Encuentros_EquipoLocalId",
                table: "Encuentros",
                newName: "IX_Encuentros_ParticipanteLocalId");

            migrationBuilder.AddColumn<bool>(
                name: "EsIndividual",
                table: "Deportes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Foto = table.Column<string>(nullable: true),
                    DeporteId = table.Column<Guid>(nullable: true),
                    EsEquipo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participantes_Deportes_DeporteId",
                        column: x => x.DeporteId,
                        principalTable: "Deportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_DeporteId",
                table: "Participantes",
                column: "DeporteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encuentros_Participantes_ParticipanteLocalId",
                table: "Encuentros",
                column: "ParticipanteLocalId",
                principalTable: "Participantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Encuentros_Participantes_ParticipanteVisitanteId",
                table: "Encuentros",
                column: "ParticipanteVisitanteId",
                principalTable: "Participantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encuentros_Participantes_ParticipanteLocalId",
                table: "Encuentros");

            migrationBuilder.DropForeignKey(
                name: "FK_Encuentros_Participantes_ParticipanteVisitanteId",
                table: "Encuentros");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropColumn(
                name: "EsIndividual",
                table: "Deportes");

            migrationBuilder.RenameColumn(
                name: "IdParticipante",
                table: "Favoritos",
                newName: "IdEquipo");

            migrationBuilder.RenameColumn(
                name: "ParticipanteVisitanteId",
                table: "Encuentros",
                newName: "EquipoVisitanteId");

            migrationBuilder.RenameColumn(
                name: "ParticipanteLocalId",
                table: "Encuentros",
                newName: "EquipoLocalId");

            migrationBuilder.RenameIndex(
                name: "IX_Encuentros_ParticipanteVisitanteId",
                table: "Encuentros",
                newName: "IX_Encuentros_EquipoVisitanteId");

            migrationBuilder.RenameIndex(
                name: "IX_Encuentros_ParticipanteLocalId",
                table: "Encuentros",
                newName: "IX_Encuentros_EquipoLocalId");

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeporteId = table.Column<Guid>(nullable: true),
                    Foto = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_DeporteId",
                table: "Equipos",
                column: "DeporteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encuentros_Equipos_EquipoLocalId",
                table: "Encuentros",
                column: "EquipoLocalId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Encuentros_Equipos_EquipoVisitanteId",
                table: "Encuentros",
                column: "EquipoVisitanteId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
