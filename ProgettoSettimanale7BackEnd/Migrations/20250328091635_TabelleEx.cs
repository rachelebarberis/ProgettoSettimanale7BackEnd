using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProgettoSettimanale7BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class TabelleEx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35e63d2c-1385-445a-ac2d-edfcf3ee7624");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51aa1aaa-b9c0-4a21-bb43-5134b26f3baa");

            migrationBuilder.CreateTable(
                name: "Artisti",
                columns: table => new
                {
                    ArtistaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artisti", x => x.ArtistaId);
                });

            migrationBuilder.CreateTable(
                name: "Eventi",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Luogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtistaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventi", x => x.EventoId);
                    table.ForeignKey(
                        name: "FK_Eventi_Artisti_ArtistaId",
                        column: x => x.ArtistaId,
                        principalTable: "Artisti",
                        principalColumn: "ArtistaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Biglietti",
                columns: table => new
                {
                    BigliettoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataAcquisto = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biglietti", x => x.BigliettoId);
                    table.ForeignKey(
                        name: "FK_Biglietti_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Biglietti_Eventi_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventi",
                        principalColumn: "EventoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41e37400-bb57-463a-be9c-f72978bcae68", "41e37400-bb57-463a-be9c-f72978bcae68", "Admin", "ADMIN" },
                    { "7203064a-0241-44da-8e40-82f85ed6f037", "7203064a-0241-44da-8e40-82f85ed6f037", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biglietti_EventoId",
                table: "Biglietti",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Biglietti_UserId",
                table: "Biglietti",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventi_ArtistaId",
                table: "Eventi",
                column: "ArtistaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biglietti");

            migrationBuilder.DropTable(
                name: "Eventi");

            migrationBuilder.DropTable(
                name: "Artisti");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41e37400-bb57-463a-be9c-f72978bcae68");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7203064a-0241-44da-8e40-82f85ed6f037");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35e63d2c-1385-445a-ac2d-edfcf3ee7624", "35e63d2c-1385-445a-ac2d-edfcf3ee7624", "User", "USER" },
                    { "51aa1aaa-b9c0-4a21-bb43-5134b26f3baa", "51aa1aaa-b9c0-4a21-bb43-5134b26f3baa", "Admin", "ADMIN" }
                });
        }
    }
}
