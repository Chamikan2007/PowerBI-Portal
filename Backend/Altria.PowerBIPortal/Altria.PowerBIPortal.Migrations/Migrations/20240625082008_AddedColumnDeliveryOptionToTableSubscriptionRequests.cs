using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altria.PowerBIPortal.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnDeliveryOptionToTableSubscriptionRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryOption",
                table: "SubscriptionRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryOption",
                table: "SubscriptionRequests");
        }
    }
}
