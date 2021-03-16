using Microsoft.EntityFrameworkCore.Migrations;

namespace DT102G_project_api.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Buzzwords",
                table: "Works",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Works",
                type: "TEXT",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Works");

            migrationBuilder.AlterColumn<string>(
                name: "Buzzwords",
                table: "Works",
                type: "TEXT",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
