using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altria.PowerBIPortal.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedScheduleTypeToTableSubscriptionRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Schedule",
                table: "SubscriptionRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleType",
                table: "SubscriptionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleType",
                table: "SubscriptionRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Schedule",
                table: "SubscriptionRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
