using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wolmart.MVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOrderTableCommentRow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Orders",
                type: "nvarchar(525)",
                maxLength: 525,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(525)",
                oldMaxLength: 525,
                oldNullable: true);
        }
    }
}
