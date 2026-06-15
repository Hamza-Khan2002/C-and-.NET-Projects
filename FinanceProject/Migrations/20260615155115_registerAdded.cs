using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceProject.Migrations
{
    /// <inheritdoc />
    public partial class registerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert roles only if they do not already exist to avoid duplicate-key errors when updating DB
            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Id = '1')
BEGIN
    INSERT INTO AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName)
    VALUES ('1', 'b50106a9-c9dd-46f4-ba0e-5922c0c34895', 'Admin', 'ADMIN')
END
");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Id = '2')
BEGIN
    INSERT INTO AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName)
    VALUES ('2', '6f328af3-8f22-4f02-bb0b-d6719ddd5bee', 'User', 'USER')
END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");
        }
    }
}
