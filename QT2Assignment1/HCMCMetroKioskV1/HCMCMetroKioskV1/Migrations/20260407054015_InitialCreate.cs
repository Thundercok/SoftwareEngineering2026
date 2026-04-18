using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCMCMetroKioskV1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FareRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromStationOrder = table.Column<int>(type: "int", nullable: false),
                    ToStationOrder = table.Column<int>(type: "int", nullable: false),
                    CashPrice = table.Column<int>(type: "int", nullable: false),
                    NonCashPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FareRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationOrder = table.Column<int>(type: "int", nullable: false),
                    IsUnderground = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsReturn = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    FromStationId = table.Column<int>(type: "int", nullable: true),
                    ToStationId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarcodeData = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Stations_FromStationId",
                        column: x => x.FromStationId,
                        principalTable: "Stations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Stations_ToStationId",
                        column: x => x.ToStationId,
                        principalTable: "Stations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FromStationId",
                table: "Tickets",
                column: "FromStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ToStationId",
                table: "Tickets",
                column: "ToStationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FareRules");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
