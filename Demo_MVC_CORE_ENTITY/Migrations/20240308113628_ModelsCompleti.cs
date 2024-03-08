using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Progetto_MVC_CORE_ENTITY.Migrations
{
    /// <inheritdoc />
    public partial class ModelsCompleti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    IdAdmin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.IdAdmin);
                });

            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodiceFiscale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Città = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cellulare = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Pensioni",
                columns: table => new
                {
                    IdPensione = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPensione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pensioni", x => x.IdPensione);
                });

            migrationBuilder.CreateTable(
                name: "TipoCamera",
                columns: table => new
                {
                    IdTipoCamera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTipoCamera = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCamera", x => x.IdTipoCamera);
                });

            migrationBuilder.CreateTable(
                name: "TipoServizio",
                columns: table => new
                {
                    IdTipoServizio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTipoServizio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoServizio", x => x.IdTipoServizio);
                });

            migrationBuilder.CreateTable(
                name: "Camera",
                columns: table => new
                {
                    IdCamera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    IdTipoCamera = table.Column<int>(type: "int", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false),
                    Disponibile = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camera", x => x.IdCamera);
                    table.ForeignKey(
                        name: "FK_Camera_TipoCamera_IdTipoCamera",
                        column: x => x.IdTipoCamera,
                        principalTable: "TipoCamera",
                        principalColumn: "IdTipoCamera",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prenotazioni",
                columns: table => new
                {
                    IdPrenotazione = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdCamera = table.Column<int>(type: "int", nullable: false),
                    IdPensione = table.Column<int>(type: "int", nullable: false),
                    DataInizio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acconto = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prenotazioni", x => x.IdPrenotazione);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Camera_IdCamera",
                        column: x => x.IdCamera,
                        principalTable: "Camera",
                        principalColumn: "IdCamera",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Clienti_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clienti",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Pensioni_IdPensione",
                        column: x => x.IdPensione,
                        principalTable: "Pensioni",
                        principalColumn: "IdPensione",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servizi",
                columns: table => new
                {
                    IdServizio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPrenotazione = table.Column<int>(type: "int", nullable: false),
                    IdTipoServizio = table.Column<int>(type: "int", nullable: false),
                    Costo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servizi", x => x.IdServizio);
                    table.ForeignKey(
                        name: "FK_Servizi_Prenotazioni_IdPrenotazione",
                        column: x => x.IdPrenotazione,
                        principalTable: "Prenotazioni",
                        principalColumn: "IdPrenotazione",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servizi_TipoServizio_IdTipoServizio",
                        column: x => x.IdTipoServizio,
                        principalTable: "TipoServizio",
                        principalColumn: "IdTipoServizio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Camera_IdTipoCamera",
                table: "Camera",
                column: "IdTipoCamera");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdCamera",
                table: "Prenotazioni",
                column: "IdCamera");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdCliente",
                table: "Prenotazioni",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdPensione",
                table: "Prenotazioni",
                column: "IdPensione");

            migrationBuilder.CreateIndex(
                name: "IX_Servizi_IdPrenotazione",
                table: "Servizi",
                column: "IdPrenotazione");

            migrationBuilder.CreateIndex(
                name: "IX_Servizi_IdTipoServizio",
                table: "Servizi",
                column: "IdTipoServizio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Servizi");

            migrationBuilder.DropTable(
                name: "Prenotazioni");

            migrationBuilder.DropTable(
                name: "TipoServizio");

            migrationBuilder.DropTable(
                name: "Camera");

            migrationBuilder.DropTable(
                name: "Clienti");

            migrationBuilder.DropTable(
                name: "Pensioni");

            migrationBuilder.DropTable(
                name: "TipoCamera");
        }
    }
}
