using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altria.PowerBIPortal.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnOwnerToTableSubscriptionRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "SubscriptionRequests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "SubscriptionRequests");
        }
    }
}
