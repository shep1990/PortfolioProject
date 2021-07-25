using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioProject.DataAccess.Migrations
{
    public partial class updateConfigurationAndAddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PortfolioEntries",
                table: "PortfolioEntries");

            migrationBuilder.RenameTable(
                name: "PortfolioEntries",
                newName: "Portfolio");

            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "Portfolio",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Portfolio",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Portfolio",
                columns: new[] { "Id", "Description", "Heading" },
                values: new object[] { 1, "Portfolio Description", "Portfolio Heading" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio");

            migrationBuilder.DeleteData(
                table: "Portfolio",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "Portfolio",
                newName: "PortfolioEntries");

            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "PortfolioEntries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PortfolioEntries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PortfolioEntries",
                table: "PortfolioEntries",
                column: "Id");
        }
    }
}
