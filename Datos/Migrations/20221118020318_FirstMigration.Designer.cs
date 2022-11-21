﻿// <auto-generated />
using System;
using Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Datos.Migrations
{
    [DbContext(typeof(ControlCalidadContexto))]
    [Migration("20221118020318_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Dominio.Entidades.Alerta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("FechaLimite")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaReinicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrdenId")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrdenId");

                    b.ToTable("Alertas", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colores", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Defecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Defectos", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dni")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rol")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Empleados", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Incidencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DefectoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("JornadaId")
                        .HasColumnType("int");

                    b.Property<int?>("Pie")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DefectoId");

                    b.HasIndex("JornadaId");

                    b.ToTable("Incidencias", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.JornadaLaboral", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrdenDeProduccionId")
                        .HasColumnType("int");

                    b.Property<int>("SupervisorDeCalidadId")
                        .HasColumnType("int");

                    b.Property<int>("TurnoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrdenDeProduccionId");

                    b.HasIndex("SupervisorDeCalidadId");

                    b.HasIndex("TurnoId");

                    b.ToTable("JornadasLaborales", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.LineaDeTrabajo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LineasDeTrabajo", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Modelo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LimiteInferiorObservado")
                        .HasColumnType("int");

                    b.Property<int>("LimiteInferiorReproceso")
                        .HasColumnType("int");

                    b.Property<int>("LimiteSuperiorObservado")
                        .HasColumnType("int");

                    b.Property<int>("LimiteSuperiorReproceso")
                        .HasColumnType("int");

                    b.Property<int>("Sku")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Modelos", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.OrdenDeProduccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LineaId")
                        .HasColumnType("int");

                    b.Property<int>("ModeloId")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupervisorDeLineaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("LineaId");

                    b.HasIndex("ModeloId");

                    b.HasIndex("SupervisorDeLineaId");

                    b.ToTable("OrdenesDeProduccion", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Turnos", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Alerta", b =>
                {
                    b.HasOne("Dominio.Entidades.OrdenDeProduccion", "Orden")
                        .WithMany("Alertas")
                        .HasForeignKey("OrdenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orden");
                });

            modelBuilder.Entity("Dominio.Entidades.Incidencia", b =>
                {
                    b.HasOne("Dominio.Entidades.Defecto", "Defecto")
                        .WithMany()
                        .HasForeignKey("DefectoId");

                    b.HasOne("Dominio.Entidades.JornadaLaboral", "Jornada")
                        .WithMany("Incidencias")
                        .HasForeignKey("JornadaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Defecto");

                    b.Navigation("Jornada");
                });

            modelBuilder.Entity("Dominio.Entidades.JornadaLaboral", b =>
                {
                    b.HasOne("Dominio.Entidades.OrdenDeProduccion", null)
                        .WithMany("Jornadas")
                        .HasForeignKey("OrdenDeProduccionId");

                    b.HasOne("Dominio.Entidades.Empleado", "SupervisorDeCalidad")
                        .WithMany("Jornada")
                        .HasForeignKey("SupervisorDeCalidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Turno", "Turno")
                        .WithMany("Jornadas")
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SupervisorDeCalidad");

                    b.Navigation("Turno");
                });

            modelBuilder.Entity("Dominio.Entidades.OrdenDeProduccion", b =>
                {
                    b.HasOne("Dominio.Entidades.Color", "Color")
                        .WithMany("OrdenesDeProduccion")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.LineaDeTrabajo", "Linea")
                        .WithMany("OrdenesDeProduccion")
                        .HasForeignKey("LineaId");

                    b.HasOne("Dominio.Entidades.Modelo", "Modelo")
                        .WithMany("OrdenesDeProduccion")
                        .HasForeignKey("ModeloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Empleado", "SupervisorDeLinea")
                        .WithMany("OrdenesDeProduccion")
                        .HasForeignKey("SupervisorDeLineaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Linea");

                    b.Navigation("Modelo");

                    b.Navigation("SupervisorDeLinea");
                });

            modelBuilder.Entity("Dominio.Entidades.Color", b =>
                {
                    b.Navigation("OrdenesDeProduccion");
                });

            modelBuilder.Entity("Dominio.Entidades.Empleado", b =>
                {
                    b.Navigation("Jornada");

                    b.Navigation("OrdenesDeProduccion");
                });

            modelBuilder.Entity("Dominio.Entidades.JornadaLaboral", b =>
                {
                    b.Navigation("Incidencias");
                });

            modelBuilder.Entity("Dominio.Entidades.LineaDeTrabajo", b =>
                {
                    b.Navigation("OrdenesDeProduccion");
                });

            modelBuilder.Entity("Dominio.Entidades.Modelo", b =>
                {
                    b.Navigation("OrdenesDeProduccion");
                });

            modelBuilder.Entity("Dominio.Entidades.OrdenDeProduccion", b =>
                {
                    b.Navigation("Alertas");

                    b.Navigation("Jornadas");
                });

            modelBuilder.Entity("Dominio.Entidades.Turno", b =>
                {
                    b.Navigation("Jornadas");
                });
#pragma warning restore 612, 618
        }
    }
}
