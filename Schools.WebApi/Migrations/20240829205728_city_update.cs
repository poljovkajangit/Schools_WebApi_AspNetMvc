using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolWebApi.Migrations
{
    /// <inheritdoc />
    public partial class city_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PercentageOfStudents",
                table: "Cities",
                type: "decimal(2,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentageOfStudents",
                table: "Cities");
        }
    }
}
