using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class zraporu2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EkmekGelir",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Gun = table.Column<DateTime>(nullable: false),
                    Kdv1 = table.Column<decimal>(nullable: false),
                    Kdv18 = table.Column<decimal>(nullable: false),
                    Kdv8 = table.Column<decimal>(nullable: false),
                    KdvToplam = table.Column<decimal>(nullable: false),
                    KdvliToplam = table.Column<decimal>(nullable: false),
                    KdvsizToplam = table.Column<decimal>(nullable: false),
                    Top1 = table.Column<decimal>(nullable: false),
                    Top18 = table.Column<decimal>(nullable: false),
                    Top8 = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkmekGelir", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EkmekGelir");
        }
    }
}
