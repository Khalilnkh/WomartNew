using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wolmart.MVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFeedbackTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Feedbacks");

            migrationBuilder.CreateTable(
                name: "FeedbackImages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    FeedbackID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FeedbackImages_Feedbacks_FeedbackID",
                        column: x => x.FeedbackID,
                        principalTable: "Feedbacks",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FeedbackImages_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackImages_FeedbackID",
                table: "FeedbackImages",
                column: "FeedbackID");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackImages_ProductID",
                table: "FeedbackImages",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackImages");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
