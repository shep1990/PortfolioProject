using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioProject.DataAccess.Migrations
{
    public partial class AddCvContentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CVContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    Heading = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CVContent_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Section",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Education" });

            migrationBuilder.InsertData(
                table: "Section",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Experiance" });

            migrationBuilder.InsertData(
                table: "CVContent",
                columns: new[] { "Id", "Content", "Heading", "SectionId" },
                values: new object[,]
                {
                    { 1, "School list", "Schools", 1 },
                    { 2, "College list", "College", 1 },
                    { 3, "University list", "University", 1 },
                    { 4, "Next experience", "Next", 2 },
                    { 5, "Gould Tech experience", "Gould Tech Experience", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CVContent_SectionId",
                table: "CVContent",
                column: "SectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CVContent");

            migrationBuilder.DropTable(
                name: "Section");
        }
    }
}
