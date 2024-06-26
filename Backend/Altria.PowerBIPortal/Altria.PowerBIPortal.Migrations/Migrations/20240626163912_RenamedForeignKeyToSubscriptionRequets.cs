using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altria.PowerBIPortal.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class RenamedForeignKeyToSubscriptionRequets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionRequestApprovalLevels_SubscriptionRequests_SubscriptionId",
                table: "SubscriptionRequestApprovalLevels");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "SubscriptionRequestApprovalLevels",
                newName: "SubscriptionRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionRequestApprovalLevels_SubscriptionId",
                table: "SubscriptionRequestApprovalLevels",
                newName: "IX_SubscriptionRequestApprovalLevels_SubscriptionRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionRequestApprovalLevels_SubscriptionRequests_SubscriptionRequestId",
                table: "SubscriptionRequestApprovalLevels",
                column: "SubscriptionRequestId",
                principalTable: "SubscriptionRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionRequestApprovalLevels_SubscriptionRequests_SubscriptionRequestId",
                table: "SubscriptionRequestApprovalLevels");

            migrationBuilder.RenameColumn(
                name: "SubscriptionRequestId",
                table: "SubscriptionRequestApprovalLevels",
                newName: "SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionRequestApprovalLevels_SubscriptionRequestId",
                table: "SubscriptionRequestApprovalLevels",
                newName: "IX_SubscriptionRequestApprovalLevels_SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionRequestApprovalLevels_SubscriptionRequests_SubscriptionId",
                table: "SubscriptionRequestApprovalLevels",
                column: "SubscriptionId",
                principalTable: "SubscriptionRequests",
                principalColumn: "Id");
        }
    }
}
