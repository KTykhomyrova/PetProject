using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedbackHub.Model.Migrations
{
    /// <inheritdoc />
    public partial class RenameUpdatedAtToModifiedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                 name: "UpdatedAt",
                 table: "Feedbacks",
                 newName: "ModifiedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                 name: "ModifiedAt",
                 table: "Feedbacks",
                 newName: "UpdatedAt");
        }
    }
}
