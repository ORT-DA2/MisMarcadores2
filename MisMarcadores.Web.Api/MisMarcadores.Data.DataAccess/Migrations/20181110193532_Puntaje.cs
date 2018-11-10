using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisMarcadores.Data.DataAccess.Migrations
{
    public partial class Puntaje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipanteEncuentro");

            migrationBuilder.CreateTable(
                name: "Puntaje",
                columns: table => new
                {
                    ParticipanteId = table.Column<Guid>(nullable: false),
                    PuntosObtenidos = table.Column<int>(nullable: false),
                    EncuentroId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puntaje", x => new { x.ParticipanteId, x.EncuentroId });
                    table.ForeignKey(
                        name: "FK_Puntaje_Encuentros_EncuentroId",
                        column: x => x.EncuentroId,
                        principalTable: "Encuentros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Puntaje_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Puntaje_EncuentroId",
                table: "Puntaje",
                column: "EncuentroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Puntaje");

            migrationBuilder.CreateTable(
                name: "ParticipanteEncuentro",
                columns: table => new
                {
                    ParticipanteId = table.Column<Guid>(nullable: false),
                    EncuentroId = table.Column<Guid>(nullable: false),
                    Puntaje = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipanteEncuentro", x => new { x.ParticipanteId, x.EncuentroId });
                    table.ForeignKey(
                        name: "FK_ParticipanteEncuentro_Encuentros_EncuentroId",
                        column: x => x.EncuentroId,
                        principalTable: "Encuentros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipanteEncuentro_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanteEncuentro_EncuentroId",
                table: "ParticipanteEncuentro",
                column: "EncuentroId");
        }
    }
}
