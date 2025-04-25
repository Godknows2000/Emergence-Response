using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmergenceResponse.Data.Migrations
{
    /// <inheritdoc />
    public partial class voicenotereport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "AudioData",
                table: "Emergency",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AudioMimeType",
                table: "Emergency",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioData",
                table: "Emergency");

            migrationBuilder.DropColumn(
                name: "AudioMimeType",
                table: "Emergency");
        }
    }
}
