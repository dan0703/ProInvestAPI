using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProInvestAPI.Migrations
{
    /// <inheritdoc />
    public partial class notNullColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    idDocumentType = table.Column<int>(type: "int", nullable: false),
                    typeName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idDocumentType);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "InvestmentType",
                columns: table => new
                {
                    idInvestmentType = table.Column<int>(type: "int", nullable: false),
                    typeName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    anualInterestRate = table.Column<int>(type: "int", nullable: false),
                    GAT_Real = table.Column<int>(type: "int", nullable: false),
                    GAT_Nominal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idInvestmentType);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Municipality",
                columns: table => new
                {
                    idMunicipality = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idMunicipality);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "OriginOfFounds",
                columns: table => new
                {
                    idOriginOfFounds = table.Column<int>(type: "int", nullable: false),
                    nameOfOrigin = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idOriginOfFounds);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    idRequestStatus = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idRequestStatus);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    idStates = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idStates);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "InvestmentSimulator",
                columns: table => new
                {
                    idInvestmentSimulator = table.Column<int>(type: "int", nullable: false),
                    investmentType = table.Column<int>(type: "int", nullable: false),
                    investmentTerm = table.Column<int>(type: "int", nullable: false),
                    investmentAmount = table.Column<int>(type: "int", nullable: false),
                    estimatedResult = table.Column<int>(type: "int", nullable: true),
                    simulationDate = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idInvestmentSimulator);
                    table.ForeignKey(
                        name: "investmentType",
                        column: x => x.investmentType,
                        principalTable: "InvestmentType",
                        principalColumn: "idInvestmentType");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "postalCode",
                columns: table => new
                {
                    idpostalCode = table.Column<int>(type: "int", nullable: false),
                    stateId = table.Column<int>(type: "int", nullable: false),
                    municipalityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idpostalCode);
                    table.ForeignKey(
                        name: "municipalityId",
                        column: x => x.municipalityId,
                        principalTable: "Municipality",
                        principalColumn: "idMunicipality");
                    table.ForeignKey(
                        name: "stateId",
                        column: x => x.stateId,
                        principalTable: "State",
                        principalColumn: "idStates");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Neighborhood",
                columns: table => new
                {
                    idNeighborhood = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    postalCodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idNeighborhood);
                    table.ForeignKey(
                        name: "postalCodeId",
                        column: x => x.postalCodeId,
                        principalTable: "postalCode",
                        principalColumn: "idpostalCode");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Adress",
                columns: table => new
                {
                    idAdress = table.Column<int>(type: "int", nullable: false),
                    street = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    number = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    interiorNumber = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    postalCode = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    neighborhoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idAdress);
                    table.ForeignKey(
                        name: "neighborhoodId",
                        column: x => x.neighborhoodId,
                        principalTable: "Neighborhood",
                        principalColumn: "idNeighborhood");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    idClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    lastName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    RFC = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    birthDay = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    academicDegree = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    profession = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    companyName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    phoneNumber = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    adressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idClient);
                    table.ForeignKey(
                        name: "adressId",
                        column: x => x.adressId,
                        principalTable: "Adress",
                        principalColumn: "idAdress");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "InvestmentRequest",
                columns: table => new
                {
                    idInvestmentRequest = table.Column<int>(type: "int", nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    investmentFolio = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    IPAddress = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    investmentSimulatorId = table.Column<int>(type: "int", nullable: false),
                    originOfFounds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idInvestmentRequest);
                    table.ForeignKey(
                        name: "clientId",
                        column: x => x.clientId,
                        principalTable: "Client",
                        principalColumn: "idClient");
                    table.ForeignKey(
                        name: "investmentSimulatorId",
                        column: x => x.investmentSimulatorId,
                        principalTable: "InvestmentSimulator",
                        principalColumn: "idInvestmentSimulator");
                    table.ForeignKey(
                        name: "originOfFounds",
                        column: x => x.originOfFounds,
                        principalTable: "OriginOfFounds",
                        principalColumn: "idOriginOfFounds");
                    table.ForeignKey(
                        name: "requestStatusId",
                        column: x => x.status,
                        principalTable: "RequestStatus",
                        principalColumn: "idRequestStatus");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    email = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    password = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    create_time = table.Column<DateTime>(type: "timestamp", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idUser);
                    table.ForeignKey(
                        name: "cliendId",
                        column: x => x.idUser,
                        principalTable: "Client",
                        principalColumn: "idClient");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    idDocuments = table.Column<int>(type: "int", nullable: false),
                    investmentRequestId = table.Column<int>(type: "int", nullable: false),
                    documentTypeId = table.Column<int>(type: "int", nullable: false),
                    fileName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    fileFormat = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    fileSize = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    file = table.Column<byte[]>(type: "blob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idDocuments);
                    table.ForeignKey(
                        name: "documentTypeId",
                        column: x => x.documentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "idDocumentType");
                    table.ForeignKey(
                        name: "investmentRequestId",
                        column: x => x.investmentRequestId,
                        principalTable: "InvestmentRequest",
                        principalColumn: "idInvestmentRequest");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateIndex(
                name: "neighborhoodId_idx",
                table: "Adress",
                column: "neighborhoodId");

            migrationBuilder.CreateIndex(
                name: "adressId_idx",
                table: "Client",
                column: "adressId");

            migrationBuilder.CreateIndex(
                name: "documentTypeId_idx",
                table: "Document",
                column: "documentTypeId");

            migrationBuilder.CreateIndex(
                name: "investmentRequestId_idx",
                table: "Document",
                column: "investmentRequestId");

            migrationBuilder.CreateIndex(
                name: "clientId_idx",
                table: "InvestmentRequest",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "investmentSimulatorId_idx",
                table: "InvestmentRequest",
                column: "investmentSimulatorId");

            migrationBuilder.CreateIndex(
                name: "originOfFounds_idx",
                table: "InvestmentRequest",
                column: "originOfFounds");

            migrationBuilder.CreateIndex(
                name: "requestStatusId_idx",
                table: "InvestmentRequest",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "investmentType_idx",
                table: "InvestmentSimulator",
                column: "investmentType");

            migrationBuilder.CreateIndex(
                name: "postalCodeId_idx",
                table: "Neighborhood",
                column: "postalCodeId");

            migrationBuilder.CreateIndex(
                name: "municipalityId_idx",
                table: "postalCode",
                column: "municipalityId");

            migrationBuilder.CreateIndex(
                name: "stateId_idx",
                table: "postalCode",
                column: "stateId");

            migrationBuilder.CreateIndex(
                name: "email",
                table: "User",
                column: "email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "InvestmentRequest");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "InvestmentSimulator");

            migrationBuilder.DropTable(
                name: "OriginOfFounds");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropTable(
                name: "Adress");

            migrationBuilder.DropTable(
                name: "InvestmentType");

            migrationBuilder.DropTable(
                name: "Neighborhood");

            migrationBuilder.DropTable(
                name: "postalCode");

            migrationBuilder.DropTable(
                name: "Municipality");

            migrationBuilder.DropTable(
                name: "State");
        }
    }
}
