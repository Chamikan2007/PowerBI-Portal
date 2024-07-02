using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altria.PowerBIPortal.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnsSubscriptionTypeAndDescriptionToTableSubscriptionRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SubscriptionRequests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionType",
                table: "SubscriptionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SubscriptionRequests");

            migrationBuilder.DropColumn(
                name: "SubscriptionType",
                table: "SubscriptionRequests");
        }
    }
}
