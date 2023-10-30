using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ispit.Todo.Migrations
{
    /// <inheritdoc />
    public partial class initia6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TodoId",
                table: "Todolist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Task1",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AspNetId",
                table: "AspNetUser",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TodoId",
                table: "Todolist");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Task1");

            migrationBuilder.DropColumn(
                name: "AspNetId",
                table: "AspNetUser");
        }
    }
}
