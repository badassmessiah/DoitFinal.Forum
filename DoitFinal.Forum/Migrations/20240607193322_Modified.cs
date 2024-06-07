using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoitFinal.Forum.Migrations
{
    /// <inheritdoc />
    public partial class Modified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8716071C-1D9B-48FD-B3D0-F059C4FB8031",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "023a8561-dbeb-4b5d-889b-a769eb39a0b9", "AQAAAAIAAYagAAAAEOXby3HrVUsxJeZsmvv9JKGsIbpLwLMo6Kz4uBmqPJXiV11n8Bq/yA8Vyv67ETGC8A==", "4787acd2-cb55-4345-a892-964316a3242f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87746F88-DC38-4756-924A-B95CFF3A1D8A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e6ddc33-9aad-4079-92fb-8cdbe85d120f", "AQAAAAIAAYagAAAAENQqVhkpUGKm0wi3iz9iC1qHU7cmpF9qvscekvU2hfDk70FjT6KI8b2JScgmKXZyKQ==", "3d0d35fb-fc54-4629-a099-121aa214a083" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D514EDC9-94BB-416F-AF9D-7C13669689C9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8519a978-174d-4a8a-bd0f-b44c65201c78", "AQAAAAIAAYagAAAAELLWXGOKUcKyuzUCzFWBbkLwHYkLasstWowVPJ6E0HHsPQ/HLPgL+L04MNOU41mM5Q==", "6676b3b9-8adb-4489-bdea-bfedc0761ea3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Topics");

            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8716071C-1D9B-48FD-B3D0-F059C4FB8031",
                columns: new[] { "ConcurrencyStamp", "IsBanned", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47f193c5-447f-47c1-b79a-64707dd44b16", false, "AQAAAAIAAYagAAAAEG67ctVb6/Xj7zcHmO64tQP6lxWxv4MIYQOFSfAgbv67QwFGMJM7Sa51r3aD/Q5oPQ==", "be3cbb49-68e7-49b1-b6d3-1eb0ede06427" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87746F88-DC38-4756-924A-B95CFF3A1D8A",
                columns: new[] { "ConcurrencyStamp", "IsBanned", "PasswordHash", "SecurityStamp" },
                values: new object[] { "861ce4fc-901c-45a2-b604-871e1a6d650f", false, "AQAAAAIAAYagAAAAEGuT/oSc21cLsggiv+xEEMXeb6RldbG1p6wGxtsiA3lpcnJ4/BOeqnENMCCWO5funQ==", "11465591-314b-4633-8cc2-fde9012c54dc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D514EDC9-94BB-416F-AF9D-7C13669689C9",
                columns: new[] { "ConcurrencyStamp", "IsBanned", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e733c6b-797b-4ac3-b3dd-ce07c6eb9048", false, "AQAAAAIAAYagAAAAEOJyDm+SrtVOdX0e9c3Koi3XrK9wJqdjE3gVfb+u/UT4UfRJzfHzjgT4qn58GFahoQ==", "4bc93a78-750b-4498-bcb6-9c5fa3567890" });
        }
    }
}
