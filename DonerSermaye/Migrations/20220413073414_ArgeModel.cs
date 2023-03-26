using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class ArgeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "arge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aciklama = table.Column<string>(nullable: true),
                    ArgeNo = table.Column<string>(nullable: true),
                    Baslama = table.Column<DateTime>(nullable: false),
                    Bitis = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "argepersonel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArgeId = table.Column<int>(nullable: false),
                    PersonelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_argepersonel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_arge_argepersonel",
                        column: x => x.ArgeId,
                        principalTable: "arge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personel_argepersonel",
                        column: x => x.PersonelId,
                        principalTable: "personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_argepersonel_ArgeId",
                table: "argepersonel",
                column: "ArgeId");

            migrationBuilder.CreateIndex(
                name: "IX_argepersonel_PersonelId",
                table: "argepersonel",
                column: "PersonelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "argepersonel");

            migrationBuilder.DropTable(
                name: "arge");
        }
    }
}
