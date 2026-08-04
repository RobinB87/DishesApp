using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOptionalGuid_Backfill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE "Dishes" SET "Guid" = gen_random_uuid() WHERE "Guid" IS NULL;
                """);

            migrationBuilder.Sql(
                """
                UPDATE "Ingredients" SET "Guid" = gen_random_uuid() WHERE "Guid" IS NULL;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No-op: reverting a backfill by nulling guids back out isn't meaningful.
        }
    }
}
