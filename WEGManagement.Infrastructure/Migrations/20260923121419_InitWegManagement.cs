using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WEGManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitWegManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankConnection = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Craftsman = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairOrders_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Size = table.Column<float>(type: "real", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    NumberOfResidents = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartments_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apartments_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CondoFees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    ApartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondoFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CondoFees_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CondoFeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_CondoFees_CondoFeeId",
                        column: x => x.CondoFeeId,
                        principalTable: "CondoFees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Address" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000001"), "742 Evergreen Terrace, Springfield" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "BankConnection", "Contact", "Name" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), "DEMO-BANK-0001", "bugs.bunny@example.test", "Bugs Bunny" },
                    { new Guid("30000000-0000-0000-0000-000000000002"), "DEMO-BANK-0002", "leela@example.test", "Turanga Leela" }
                });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "Address", "BuildingId", "Floor", "NumberOfResidents", "OwnerId", "Size", "Status" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "742 Evergreen Terrace, Wohnung 1", new Guid("10000000-0000-0000-0000-000000000001"), 1, 2, new Guid("30000000-0000-0000-0000-000000000001"), 82.5f, 0 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "742 Evergreen Terrace, Wohnung 2", new Guid("10000000-0000-0000-0000-000000000001"), 2, 1, new Guid("30000000-0000-0000-0000-000000000002"), 96f, 1 }
                });

            migrationBuilder.InsertData(
                table: "RepairOrders",
                columns: new[] { "Id", "BuildingId", "Cost", "Craftsman", "Date", "Description", "Priority", "Status" },
                values: new object[,]
                {
                    { new Guid("60000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001"), 1250.00m, "Acme Roofing", new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dachrinne am Nordflügel prüfen und reparieren", 1, 0 },
                    { new Guid("60000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000001"), 680.00m, "Planet Express Maintenance", new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aufzug: Türsensor austauschen", 1, 1 },
                    { new Guid("60000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000001"), 420.00m, "MomCorp Services", new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heizungsanlage warten", 0, 2 }
                });

            migrationBuilder.InsertData(
                table: "CondoFees",
                columns: new[] { "Id", "Amount", "ApartmentId", "DueDate", "PaymentStatus" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), 285.00m, new Guid("20000000-0000-0000-0000-000000000001"), new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("40000000-0000-0000-0000-000000000002"), 285.00m, new Guid("20000000-0000-0000-0000-000000000001"), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("40000000-0000-0000-0000-000000000003"), 340.00m, new Guid("20000000-0000-0000-0000-000000000002"), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "CondoFeeId", "Date", "PaymentType", "Reference" },
                values: new object[] { new Guid("50000000-0000-0000-0000-000000000001"), 285.00m, new Guid("40000000-0000-0000-0000-000000000001"), new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "40000000-0000-0000-0000-000000000001" });

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_BuildingId",
                table: "Apartments",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_OwnerId",
                table: "Apartments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CondoFees_ApartmentId",
                table: "CondoFees",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CondoFeeId",
                table: "Payments",
                column: "CondoFeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_BuildingId",
                table: "RepairOrders",
                column: "BuildingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RepairOrders");

            migrationBuilder.DropTable(
                name: "CondoFees");

            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
