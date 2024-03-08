﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Progetto_MVC_CORE_ENTITY.Data;

#nullable disable

namespace Progetto_MVC_CORE_ENTITY.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240308113628_ModelsCompleti")]
    partial class ModelsCompleti
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Admin", b =>
                {
                    b.Property<int>("IdAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAdmin"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAdmin");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Camera", b =>
                {
                    b.Property<int>("IdCamera")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCamera"));

                    b.Property<double>("Costo")
                        .HasColumnType("float");

                    b.Property<bool>("Disponibile")
                        .HasColumnType("bit");

                    b.Property<int>("IdTipoCamera")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("IdCamera");

                    b.HasIndex("IdTipoCamera");

                    b.ToTable("Camera");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"));

                    b.Property<string>("Cellulare")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Città")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodiceFiscale")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCliente");

                    b.ToTable("Clienti");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Pensione", b =>
                {
                    b.Property<int>("IdPensione")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPensione"));

                    b.Property<double>("Costo")
                        .HasColumnType("float");

                    b.Property<string>("TipoPensione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPensione");

                    b.ToTable("Pensioni");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Prenotazione", b =>
                {
                    b.Property<int>("IdPrenotazione")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPrenotazione"));

                    b.Property<double>("Acconto")
                        .HasColumnType("float");

                    b.Property<DateTime>("DataFine")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInizio")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCamera")
                        .HasColumnType("int");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdPensione")
                        .HasColumnType("int");

                    b.HasKey("IdPrenotazione");

                    b.HasIndex("IdCamera");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdPensione");

                    b.ToTable("Prenotazioni");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Servizio", b =>
                {
                    b.Property<int>("IdServizio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdServizio"));

                    b.Property<int>("Costo")
                        .HasColumnType("int");

                    b.Property<int>("IdPrenotazione")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoServizio")
                        .HasColumnType("int");

                    b.HasKey("IdServizio");

                    b.HasIndex("IdPrenotazione");

                    b.HasIndex("IdTipoServizio");

                    b.ToTable("Servizi");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.TipoCamera", b =>
                {
                    b.Property<int>("IdTipoCamera")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTipoCamera"));

                    b.Property<string>("NomeTipoCamera")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTipoCamera");

                    b.ToTable("TipoCamera");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.TipoServizio", b =>
                {
                    b.Property<int>("IdTipoServizio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTipoServizio"));

                    b.Property<string>("NomeTipoServizio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTipoServizio");

                    b.ToTable("TipoServizio");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Camera", b =>
                {
                    b.HasOne("Progetto_MVC_CORE_ENTITY.Models.TipoCamera", "TipoCamera")
                        .WithMany("Camere")
                        .HasForeignKey("IdTipoCamera")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoCamera");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Prenotazione", b =>
                {
                    b.HasOne("Progetto_MVC_CORE_ENTITY.Models.Camera", "Camera")
                        .WithMany("Prenotazioni")
                        .HasForeignKey("IdCamera")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Progetto_MVC_CORE_ENTITY.Models.Cliente", "Cliente")
                        .WithMany("Prenotazioni")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Progetto_MVC_CORE_ENTITY.Models.Pensione", "Pensione")
                        .WithMany("Prenotazioni")
                        .HasForeignKey("IdPensione")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Camera");

                    b.Navigation("Cliente");

                    b.Navigation("Pensione");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Servizio", b =>
                {
                    b.HasOne("Progetto_MVC_CORE_ENTITY.Models.Prenotazione", "Prenotazione")
                        .WithMany("Servizi")
                        .HasForeignKey("IdPrenotazione")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Progetto_MVC_CORE_ENTITY.Models.TipoServizio", "TipoServizio")
                        .WithMany("Servizi")
                        .HasForeignKey("IdTipoServizio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prenotazione");

                    b.Navigation("TipoServizio");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Camera", b =>
                {
                    b.Navigation("Prenotazioni");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Cliente", b =>
                {
                    b.Navigation("Prenotazioni");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Pensione", b =>
                {
                    b.Navigation("Prenotazioni");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.Prenotazione", b =>
                {
                    b.Navigation("Servizi");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.TipoCamera", b =>
                {
                    b.Navigation("Camere");
                });

            modelBuilder.Entity("Progetto_MVC_CORE_ENTITY.Models.TipoServizio", b =>
                {
                    b.Navigation("Servizi");
                });
#pragma warning restore 612, 618
        }
    }
}
