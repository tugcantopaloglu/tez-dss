using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class ayrintiIstek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "istekler",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IstekAd",
                table: "istekler",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IstekAdet",
                table: "istekler",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IstekAyrinti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IstekAd = table.Column<string>(nullable: true),
                    IstekAdet = table.Column<int>(nullable: true),
                    IstekId = table.Column<int>(nullable: false),
                    YaklasikMaliyet = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IstekAyrinti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IstekAyrinti_istekler_IstekId",
                        column: x => x.IstekId,
                        principalTable: "istekler",
                        principalColumn: "IstekNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IstekAyrinti_IstekId",
                table: "IstekAyrinti",
                column: "IstekId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IstekAyrinti");

            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "istekler");

            migrationBuilder.DropColumn(
                name: "IstekAd",
                table: "istekler");

            migrationBuilder.DropColumn(
                name: "IstekAdet",
                table: "istekler");
        }
    }
}
