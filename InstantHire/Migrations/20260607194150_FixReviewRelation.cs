using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstantHire.Migrations
{
    /// <inheritdoc />
    public partial class FixReviewRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FreelancerId1",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_FreelancerId1",
                table: "Reviews",
                column: "FreelancerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Freelancers_FreelancerId1",
                table: "Reviews",
                column: "FreelancerId1",
                principalTable: "Freelancers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Freelancers_FreelancerId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_FreelancerId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "FreelancerId1",
                table: "Reviews");
        }
    }
}
