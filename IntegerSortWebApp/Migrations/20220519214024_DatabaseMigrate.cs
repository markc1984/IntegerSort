using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegerSortWebApp.Migrations
{
    public partial class DatabaseMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Numbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Integer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SortPerformance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SortTime = table.Column<long>(type: "bigint", nullable: false),
                    SortOrder = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SortPerformance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SortPerformance_Numbers_Id",
                        column: x => x.Id,
                        principalTable: "Numbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SortPerformance");

            migrationBuilder.DropTable(
                name: "Numbers");
        }
    }
}
