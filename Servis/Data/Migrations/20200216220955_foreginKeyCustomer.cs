using Microsoft.EntityFrameworkCore.Migrations;

namespace Servis.Data.Migrations
{
    public partial class foreginKeyCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "ServiceOrder",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOrder_CustomerId",
                table: "ServiceOrder",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOrder_AspNetUsers_CustomerId",
                table: "ServiceOrder",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOrder_AspNetUsers_CustomerId",
                table: "ServiceOrder");

            migrationBuilder.DropIndex(
                name: "IX_ServiceOrder_CustomerId",
                table: "ServiceOrder");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "ServiceOrder",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
