using Microsoft.EntityFrameworkCore.Migrations;

namespace Servis.Data.Migrations
{
    public partial class addCustomerspecifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Customer",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer",
                table: "AspNetUsers");
        }
    }
}
