using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class insert_new_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_M_University",
                columns: table => new
                {
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_University", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "Tb_T_Employee",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_Employee", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Tb_T_Employee_Tb_M_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "Tb_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_Education",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gpa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId1 = table.Column<int>(type: "int", nullable: true),
                    NIK = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_Education", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_TB_T_Education_Tb_M_University_UniversityId1",
                        column: x => x.UniversityId1,
                        principalTable: "Tb_M_University",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tb_T_Profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_Profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Tb_T_Profiling_TB_T_Education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "TB_T_Education",
                        principalColumn: "EducationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_T_Profiling_Tb_T_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "Tb_T_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Education_UniversityId1",
                table: "TB_T_Education",
                column: "UniversityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_Profiling_EducationId",
                table: "Tb_T_Profiling",
                column: "EducationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_T_Profiling");

            migrationBuilder.DropTable(
                name: "TB_T_Education");

            migrationBuilder.DropTable(
                name: "Tb_T_Employee");

            migrationBuilder.DropTable(
                name: "Tb_M_University");
        }
    }
}
