using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altria.PowerBIPortal.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class RenamedReportToReportPathInSubscriptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Report",
                table: "Subscription");

            migrationBuilder.AddColumn<string>(
                name: "ReportPath",
                table: "Subscription",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportPath",
                table: "Subscription");

            migrationBuilder.AddColumn<string>(
                name: "Report",
                table: "Subscription",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
