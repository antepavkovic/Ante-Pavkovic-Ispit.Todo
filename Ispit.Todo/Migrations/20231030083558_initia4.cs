using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ispit.Todo.Migrations
{
    /// <inheritdoc />
    public partial class initia4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUser",
                columns: table => new
                {
                    AspNetUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUser", x => x.AspNetUserId);
                });

            migrationBuilder.CreateTable(
                name: "Todolist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodoTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    fkTodoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todolist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todolist_AspNetUser_fkTodoId",
                        column: x => x.fkTodoId,
                        principalTable: "AspNetUser",
                        principalColumn: "AspNetUserId");
                });

            migrationBuilder.CreateTable(
                name: "Task1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    TaskTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fkId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task1_Todolist_fkId",
                        column: x => x.fkId,
                        principalTable: "Todolist",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task1_fkId",
                table: "Task1",
                column: "fkId");

            migrationBuilder.CreateIndex(
                name: "IX_Todolist_fkTodoId",
                table: "Todolist",
                column: "fkTodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task1");

            migrationBuilder.DropTable(
                name: "Todolist");

            migrationBuilder.DropTable(
                name: "AspNetUser");
        }
    }
}
