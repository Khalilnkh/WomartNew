using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wolmart.MVC.Migrations
{
    /// <inheritdoc />
    public partial class CreatedMessageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Award",
                table: "Award");

            migrationBuilder.RenameTable(
                name: "Award",
                newName: "Awards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Awards",
                table: "Awards",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Awards",
                table: "Awards");

            migrationBuilder.RenameTable(
                name: "Awards",
                newName: "Award");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Award",
                table: "Award",
                column: "ID");
        }
    }
}
