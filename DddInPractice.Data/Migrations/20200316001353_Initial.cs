using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DddInPractice.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SnackMachines",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OneCentCount = table.Column<int>(nullable: true, defaultValue: 0),
                    TenCentCount = table.Column<int>(nullable: true, defaultValue: 0),
                    QuarterCount = table.Column<int>(nullable: true, defaultValue: 0),
                    OneDollarCount = table.Column<int>(nullable: true, defaultValue: 0),
                    FiveDollarCount = table.Column<int>(nullable: true, defaultValue: 0),
                    TwentyDollarCount = table.Column<int>(nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnackMachines", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnackMachines");
        }
    }
}
