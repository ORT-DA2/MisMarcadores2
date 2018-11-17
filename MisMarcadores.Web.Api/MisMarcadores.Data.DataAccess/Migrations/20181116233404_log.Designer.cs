﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MisMarcadores.Data.DataAccess;

namespace MisMarcadores.Data.DataAccess.Migrations
{
    [DbContext(typeof(MisMarcadoresContext))]
    [Migration("20181116233404_log")]
    partial class log
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MisMarcadores.Data.Entities.Comentario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdEncuentro");

                    b.Property<string>("NombreUsuario");

                    b.Property<string>("Texto");

                    b.HasKey("Id");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("MisMarcadores.Data.Entities.Deporte", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("EsIndividual");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Deportes");
                });

            modelBuilder.Entity("MisMarcadores.Data.Entities.Encuentro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DeporteId");

                    b.Property<DateTime>("FechaHora");

                    b.HasKey("Id");

                    b.HasIndex("DeporteId");

                    b.ToTable("Encuentros");
                });

            modelBuilder.Entity("MisMarcadores.Data.Entities.Favorito", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdParticipante");

                    b.Property<string>("NombreUsuario");

                    b.HasKey("Id");

                    b.ToTable("Favoritos");
                });

            modelBuilder.Entity("MisMarcadores.Data.Entities.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Accion");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Usuario");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("MisMarcadores.Data.Entities.Participante", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DeporteId");

                    b.Property<bool>("EsEquipo");

                    b.Property<string>("Foto");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.HasIndex("DeporteId");

                    b.ToTable("Participantes");
                });

            modelBuilder.Entity("MisMarcadores.Data.Entities.ParticipanteEncuentro", b =>
                {
                    b.Property<Guid>("ParticipanteId");

                    b.Property<Guid>("EncuentroId");

                    b.Property<int>("PuntosObtenidos");

                    b.HasKey("ParticipanteId", "EncuentroId");

                    b.HasIndex("EncuentroId");

                    b.ToTable("ParticipanteEncuentro");
                });

            modelBuilder.Entity("MisMarcadores.Data.Entities.Sesion", b =>
                {
                    b.Property<string>("NombreUsuario")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("Token");

                    b.HasKey("NombreUsuario");

                    b.ToTable("Sesiones");
                });

            modelBuilder.Entity("MisMarcadores.Data.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Administrador");

                    b.Property<string>("Apellido");

                    b.Property<string>("Contraseña");

                    b.Property<string>("Mail");

                    b.Property<string>("Nombre");

                    b.Property<string>("NombreUsuario");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MisMarcadores.Data.Entities.Encuentro", b =>
                {
                    b.HasOne("MisMarcadores.Data.Entities.Deporte", "Deporte")
                        .WithMany()
                        .HasForeignKey("DeporteId");
                });

            modelBuilder.Entity("MisMarcadores.Data.Entities.Participante", b =>
                {
                    b.HasOne("MisMarcadores.Data.Entities.Deporte", "Deporte")
                        .WithMany()
                        .HasForeignKey("DeporteId");
                });

            modelBuilder.Entity("MisMarcadores.Data.Entities.ParticipanteEncuentro", b =>
                {
                    b.HasOne("MisMarcadores.Data.Entities.Encuentro", "Encuentro")
                        .WithMany("ParticipanteEncuentro")
                        .HasForeignKey("EncuentroId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MisMarcadores.Data.Entities.Participante", "Participante")
                        .WithMany("ParticipanteEncuentro")
                        .HasForeignKey("ParticipanteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
