using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormFactors",
                columns: table => new
                {
                    FormFactorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FormFactorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFactors", x => x.FormFactorId);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    PurchasePrice = table.Column<double>(nullable: false),
                    RetailPrice = table.Column<double>(nullable: false),
                    Margin = table.Column<double>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                });

            migrationBuilder.CreateTable(
                name: "RamTypes",
                columns: table => new
                {
                    RamTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RamTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RamTypes", x => x.RamTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Sockets",
                columns: table => new
                {
                    SocketId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SocketName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sockets", x => x.SocketId);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartId = table.Column<int>(nullable: true),
                    FormFactorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_FormFactors_FormFactorId",
                        column: x => x.FormFactorId,
                        principalTable: "FormFactors",
                        principalColumn: "FormFactorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cases_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GraphicsCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartId = table.Column<int>(nullable: true),
                    ClockFreq = table.Column<double>(nullable: false),
                    Gb = table.Column<int>(nullable: false),
                    RamType = table.Column<string>(nullable: true),
                    CrossSli = table.Column<bool>(nullable: false),
                    Rgb = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GraphicsCards_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperatingSystems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartId = table.Column<int>(nullable: true),
                    Size = table.Column<double>(nullable: false),
                    OpenSource = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperatingSystems_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartId = table.Column<int>(nullable: true),
                    Power = table.Column<int>(nullable: false),
                    Modular = table.Column<bool>(nullable: false),
                    PowerRating = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerSupplies_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StorageDevices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartId = table.Column<int>(nullable: true),
                    Gb = table.Column<int>(nullable: false),
                    Tb = table.Column<int>(nullable: false),
                    Ssd = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageDevices_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartId = table.Column<int>(nullable: true),
                    RamTypeId = table.Column<int>(nullable: true),
                    Gb = table.Column<int>(nullable: false),
                    StickCount = table.Column<int>(nullable: false),
                    ClockFreq = table.Column<int>(nullable: false),
                    Rgb = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rams_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rams_RamTypes_RamTypeId",
                        column: x => x.RamTypeId,
                        principalTable: "RamTypes",
                        principalColumn: "RamTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Motherboards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartId = table.Column<int>(nullable: true),
                    SocketId = table.Column<int>(nullable: true),
                    FormFactorId = table.Column<int>(nullable: true),
                    RamTypeId = table.Column<int>(nullable: true),
                    Chipset = table.Column<string>(nullable: true),
                    Oc = table.Column<bool>(nullable: false),
                    Rgb = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motherboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motherboards_FormFactors_FormFactorId",
                        column: x => x.FormFactorId,
                        principalTable: "FormFactors",
                        principalColumn: "FormFactorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motherboards_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motherboards_RamTypes_RamTypeId",
                        column: x => x.RamTypeId,
                        principalTable: "RamTypes",
                        principalColumn: "RamTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motherboards_Sockets_SocketId",
                        column: x => x.SocketId,
                        principalTable: "Sockets",
                        principalColumn: "SocketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessorCoolers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartId = table.Column<int>(nullable: true),
                    SocketId = table.Column<int>(nullable: true),
                    Rgb = table.Column<bool>(nullable: false),
                    Water = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessorCoolers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessorCoolers_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessorCoolers_Sockets_SocketId",
                        column: x => x.SocketId,
                        principalTable: "Sockets",
                        principalColumn: "SocketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Processors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartId = table.Column<int>(nullable: true),
                    SocketId = table.Column<int>(nullable: true),
                    Cores = table.Column<int>(nullable: false),
                    Threads = table.Column<int>(nullable: false),
                    ClockFreq = table.Column<int>(nullable: false),
                    TurboFreq = table.Column<int>(nullable: false),
                    Oc = table.Column<bool>(nullable: false),
                    Graph = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processors_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processors_Sockets_SocketId",
                        column: x => x.SocketId,
                        principalTable: "Sockets",
                        principalColumn: "SocketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_FormFactorId",
                table: "Cases",
                column: "FormFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_PartId",
                table: "Cases",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_GraphicsCards_PartId",
                table: "GraphicsCards",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_FormFactorId",
                table: "Motherboards",
                column: "FormFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_PartId",
                table: "Motherboards",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_RamTypeId",
                table: "Motherboards",
                column: "RamTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_SocketId",
                table: "Motherboards",
                column: "SocketId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatingSystems_PartId",
                table: "OperatingSystems",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplies_PartId",
                table: "PowerSupplies",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessorCoolers_PartId",
                table: "ProcessorCoolers",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessorCoolers_SocketId",
                table: "ProcessorCoolers",
                column: "SocketId");

            migrationBuilder.CreateIndex(
                name: "IX_Processors_PartId",
                table: "Processors",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Processors_SocketId",
                table: "Processors",
                column: "SocketId");

            migrationBuilder.CreateIndex(
                name: "IX_Rams_PartId",
                table: "Rams",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Rams_RamTypeId",
                table: "Rams",
                column: "RamTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageDevices_PartId",
                table: "StorageDevices",
                column: "PartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "GraphicsCards");

            migrationBuilder.DropTable(
                name: "Motherboards");

            migrationBuilder.DropTable(
                name: "OperatingSystems");

            migrationBuilder.DropTable(
                name: "PowerSupplies");

            migrationBuilder.DropTable(
                name: "ProcessorCoolers");

            migrationBuilder.DropTable(
                name: "Processors");

            migrationBuilder.DropTable(
                name: "Rams");

            migrationBuilder.DropTable(
                name: "StorageDevices");

            migrationBuilder.DropTable(
                name: "FormFactors");

            migrationBuilder.DropTable(
                name: "Sockets");

            migrationBuilder.DropTable(
                name: "RamTypes");

            migrationBuilder.DropTable(
                name: "Parts");
        }
    }
}
