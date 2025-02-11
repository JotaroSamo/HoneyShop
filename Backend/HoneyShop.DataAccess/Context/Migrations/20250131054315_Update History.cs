using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoneyShop.DataAccess.Context.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "History",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "History");
        }
    }
}
