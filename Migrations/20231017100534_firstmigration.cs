using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippeeAPI.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Annoucement_Status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    status = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annoucement_Status", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chat_Status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    status = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_Status", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Diplomes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    diplome = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diplomes", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Effectives",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Effectives", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Naf_Sections",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Naf_Sections", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Type_Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type_Users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    siren = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idnaf = table.Column<int>(name: "id_naf", type: "int", nullable: true),
                    picture = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    street = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cp = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    legalform = table.Column<string>(name: "legal_form", type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ideffective = table.Column<int>(name: "id_effective", type: "int", nullable: true),
                    website = table.Column<string>(name: "web_site", type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    payment = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.siren);
                    table.ForeignKey(
                        name: "FK_Companies_Effectives_id_effective",
                        column: x => x.ideffective,
                        principalTable: "Effectives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Companies_Naf_Sections_id_naf",
                        column: x => x.idnaf,
                        principalTable: "Naf_Sections",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Naf_Divisions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idnafsection = table.Column<int>(name: "id_naf_section", type: "int", nullable: true),
                    title = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Naf_Divisions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Naf_Divisions_Naf_Sections_id_naf_section",
                        column: x => x.idnafsection,
                        principalTable: "Naf_Sections",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    surname = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    firstname = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    picture = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isonline = table.Column<bool>(name: "is_online", type: "tinyint(1)", nullable: true),
                    idtypeuser = table.Column<int>(name: "id_type_user", type: "int", nullable: true),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    website = table.Column<string>(name: "web_site", type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cv = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cp = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    birthday = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    isconveyed = table.Column<bool>(name: "is_conveyed", type: "tinyint(1)", nullable: true),
                    idcompany = table.Column<int>(name: "id_company", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Companies_id_company",
                        column: x => x.idcompany,
                        principalTable: "Companies",
                        principalColumn: "siren");
                    table.ForeignKey(
                        name: "FK_Users_Type_Users_id_type_user",
                        column: x => x.idtypeuser,
                        principalTable: "Type_Users",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Annoucements",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    iduser = table.Column<int>(name: "id_user", type: "int", nullable: true),
                    title = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    publishdate = table.Column<DateTime>(name: "publish_date", type: "datetime(6)", nullable: true),
                    idtype = table.Column<int>(name: "id_type", type: "int", nullable: true),
                    idstatus = table.Column<int>(name: "id_status", type: "int", nullable: true),
                    idnafdivision = table.Column<int>(name: "id_naf_division", type: "int", nullable: true),
                    iddiplome = table.Column<int>(name: "id_diplome", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annoucements", x => x.id);
                    table.ForeignKey(
                        name: "FK_Annoucements_Annoucement_Status_id_status",
                        column: x => x.idstatus,
                        principalTable: "Annoucement_Status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Annoucements_Diplomes_id_diplome",
                        column: x => x.iddiplome,
                        principalTable: "Diplomes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Annoucements_Naf_Divisions_id_naf_division",
                        column: x => x.idnafdivision,
                        principalTable: "Naf_Divisions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Annoucements_Type_Users_id_type",
                        column: x => x.idtype,
                        principalTable: "Type_Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Annoucements_Users_id_user",
                        column: x => x.iduser,
                        principalTable: "Users",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idsender = table.Column<int>(name: "id_sender", type: "int", nullable: true),
                    idrecipient = table.Column<int>(name: "id_recipient", type: "int", nullable: true),
                    content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sendtime = table.Column<DateTime>(name: "send_time", type: "datetime(6)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.id);
                    table.ForeignKey(
                        name: "FK_Chats_Chat_Status_status",
                        column: x => x.status,
                        principalTable: "Chat_Status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Chats_Users_id_recipient",
                        column: x => x.idrecipient,
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Chats_Users_id_sender",
                        column: x => x.idsender,
                        principalTable: "Users",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recents_Searches",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    iduser = table.Column<int>(name: "id_user", type: "int", nullable: true),
                    text = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recents_Searches", x => x.id);
                    table.ForeignKey(
                        name: "FK_Recents_Searches_Users_id_user",
                        column: x => x.iduser,
                        principalTable: "Users",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Student_Skills",
                columns: table => new
                {
                    iduser = table.Column<int>(name: "id_user", type: "int", nullable: false),
                    idskill = table.Column<int>(name: "id_skill", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Skills", x => new { x.iduser, x.idskill });
                    table.ForeignKey(
                        name: "FK_Student_Skills_Skills_id_skill",
                        column: x => x.idskill,
                        principalTable: "Skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Skills_Users_id_user",
                        column: x => x.iduser,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    iduser = table.Column<int>(name: "id_user", type: "int", nullable: false),
                    idannoucement = table.Column<int>(name: "id_annoucement", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => new { x.iduser, x.idannoucement });
                    table.ForeignKey(
                        name: "FK_Favorites_Annoucements_id_annoucement",
                        column: x => x.idannoucement,
                        principalTable: "Annoucements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_id_user",
                        column: x => x.iduser,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    idannoucement = table.Column<int>(name: "id_annoucement", type: "int", nullable: false),
                    idskill = table.Column<int>(name: "id_skill", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => new { x.idannoucement, x.idskill });
                    table.ForeignKey(
                        name: "FK_Qualifications_Annoucements_id_annoucement",
                        column: x => x.idannoucement,
                        principalTable: "Annoucements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Qualifications_Skills_id_skill",
                        column: x => x.idskill,
                        principalTable: "Skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recents",
                columns: table => new
                {
                    iduser = table.Column<int>(name: "id_user", type: "int", nullable: false),
                    idannoucement = table.Column<int>(name: "id_annoucement", type: "int", nullable: false),
                    consultdate = table.Column<DateTime>(name: "consult_date", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recents", x => new { x.iduser, x.idannoucement });
                    table.ForeignKey(
                        name: "FK_Recents_Annoucements_id_annoucement",
                        column: x => x.idannoucement,
                        principalTable: "Annoucements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recents_Users_id_user",
                        column: x => x.iduser,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Annoucements_id_diplome",
                table: "Annoucements",
                column: "id_diplome");

            migrationBuilder.CreateIndex(
                name: "IX_Annoucements_id_naf_division",
                table: "Annoucements",
                column: "id_naf_division");

            migrationBuilder.CreateIndex(
                name: "IX_Annoucements_id_status",
                table: "Annoucements",
                column: "id_status");

            migrationBuilder.CreateIndex(
                name: "IX_Annoucements_id_type",
                table: "Annoucements",
                column: "id_type");

            migrationBuilder.CreateIndex(
                name: "IX_Annoucements_id_user",
                table: "Annoucements",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_id_recipient",
                table: "Chats",
                column: "id_recipient");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_id_sender",
                table: "Chats",
                column: "id_sender");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_status",
                table: "Chats",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_id_effective",
                table: "Companies",
                column: "id_effective");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_id_naf",
                table: "Companies",
                column: "id_naf");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_id_annoucement",
                table: "Favorites",
                column: "id_annoucement");

            migrationBuilder.CreateIndex(
                name: "IX_Naf_Divisions_id_naf_section",
                table: "Naf_Divisions",
                column: "id_naf_section");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_id_skill",
                table: "Qualifications",
                column: "id_skill");

            migrationBuilder.CreateIndex(
                name: "IX_Recents_id_annoucement",
                table: "Recents",
                column: "id_annoucement");

            migrationBuilder.CreateIndex(
                name: "IX_Recents_Searches_id_user",
                table: "Recents_Searches",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Skills_id_skill",
                table: "Student_Skills",
                column: "id_skill");

            migrationBuilder.CreateIndex(
                name: "IX_Users_id_company",
                table: "Users",
                column: "id_company");

            migrationBuilder.CreateIndex(
                name: "IX_Users_id_type_user",
                table: "Users",
                column: "id_type_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "Recents");

            migrationBuilder.DropTable(
                name: "Recents_Searches");

            migrationBuilder.DropTable(
                name: "Student_Skills");

            migrationBuilder.DropTable(
                name: "Chat_Status");

            migrationBuilder.DropTable(
                name: "Annoucements");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Annoucement_Status");

            migrationBuilder.DropTable(
                name: "Diplomes");

            migrationBuilder.DropTable(
                name: "Naf_Divisions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Type_Users");

            migrationBuilder.DropTable(
                name: "Effectives");

            migrationBuilder.DropTable(
                name: "Naf_Sections");
        }
    }
}
