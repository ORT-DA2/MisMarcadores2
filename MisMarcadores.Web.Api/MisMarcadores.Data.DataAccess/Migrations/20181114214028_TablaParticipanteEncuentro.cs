using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisMarcadores.Data.DataAccess.Migrations
{
    public partial class TablaParticipanteEncuentro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParticipanteEncuentro",
                columns: table => new
                {
                    ParticipanteId = table.Column<Guid>(nullable: false),
                    PuntosObtenidos = table.Column<int>(nullable: false),
                    EncuentroId = table.Column<Guid>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipanteEncuentro");
        }
    }
}
