using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTL3_MVT.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ethnicity",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Work",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Persons",
                newName: "WorkId");

            migrationBuilder.AddColumn<int>(
                name: "EthnicityId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ethnicity",
                columns: table => new
                {
                    EthnicityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EthnicityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ethnicity", x => x.EthnicityId);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    WorkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.WorkId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_EthnicityId",
                table: "Persons",
                column: "EthnicityId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_WorkId",
                table: "Persons",
                column: "WorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Ethnicity_EthnicityId",
                table: "Persons",
                column: "EthnicityId",
                principalTable: "Ethnicity",
                principalColumn: "EthnicityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Works_WorkId",
                table: "Persons",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Ethnicity_EthnicityId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Works_WorkId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Ethnicity");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Persons_EthnicityId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_WorkId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "EthnicityId",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "WorkId",
                table: "Persons",
                newName: "Age");

            migrationBuilder.AddColumn<string>(
                name: "Ethnicity",
                table: "Persons",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Work",
                table: "Persons",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
