using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolWebApi.Migrations
{
    /// <inheritdoc />
    public partial class spGetCapitolCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetCapitolCities]
                    @IsCapitol bit
                    AS
                    BEGIN
                        SET NOCOUNT ON;
                        Select * From CITIES Where IsCapitol = @IsCapitol
                    END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
