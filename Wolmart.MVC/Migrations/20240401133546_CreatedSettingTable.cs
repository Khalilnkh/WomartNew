using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wolmart.MVC.Migrations
{
    /// <inheritdoc />
    public partial class CreatedSettingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setttings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(455)", maxLength: 455, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setttings", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setttings");
        }
    }
}
