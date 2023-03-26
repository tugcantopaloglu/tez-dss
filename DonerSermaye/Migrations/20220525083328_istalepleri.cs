using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class istalepleri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "istalepleri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aciklama = table.Column<string>(nullable: true),
                    bolumId = table.Column<int>(nullable: true),
                    bolumOnay = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    firmaId = table.Column<int>(nullable: true),
                    PersonelId = table.Column<int>(nullable: true),
                    turId = table.Column<int>(nullable: true),
                    tutar = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_istalepleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_istalepleri_bolumler",
                        column: x => x.bolumId,
                        principalTable: "bolumler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_istalepleri_firmalar",
                        column: x => x.firmaId,
                        principalTable: "firmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_istalepleri_personeller",
                        column: x => x.PersonelId,
                        principalTable: "personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_istalepleri_isTurleri",
                        column: x => x.turId,
                        principalTable: "isTurleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_istalepleri_bolumId",
                table: "istalepleri",
                column: "bolumId");

            migrationBuilder.CreateIndex(
                name: "IX_istalepleri_firmaId",
                table: "istalepleri",
                column: "firmaId");

            migrationBuilder.CreateIndex(
                name: "IX_istalepleri_PersonelId",
                table: "istalepleri",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_istalepleri_turId",
                table: "istalepleri",
                column: "turId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "istalepleri");
        }
    }
}
