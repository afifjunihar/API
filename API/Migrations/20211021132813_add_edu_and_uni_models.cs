using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_edu_and_uni_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Education_Education_Id",
                table: "Profilings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Education",
                table: "Education");

            migrationBuilder.RenameTable(
                name: "Education",
                newName: "Educations");

            migrationBuilder.AlterColumn<int>(
                name: "University_Id",
                table: "Educations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                table: "Educations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Educations_University_Id",
                table: "Educations",
                column: "University_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Universities_University_Id",
                table: "Educations",
                column: "University_Id",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Educations_Education_Id",
                table: "Profilings",
                column: "Education_Id",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Universities_University_Id",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Educations_Education_Id",
                table: "Profilings");

            migrationBuilder.DropTable(
                name: "Universities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_University_Id",
                table: "Educations");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "Education");

            migrationBuilder.AlterColumn<string>(
                name: "University_Id",
                table: "Education",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Education",
                table: "Education",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Education_Education_Id",
                table: "Profilings",
                column: "Education_Id",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
