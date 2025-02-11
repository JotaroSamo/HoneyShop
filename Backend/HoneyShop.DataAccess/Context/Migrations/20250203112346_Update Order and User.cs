using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoneyShop.DataAccess.Context.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Orders",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Orders");
        }
    }
}
