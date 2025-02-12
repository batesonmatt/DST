using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DST.Migrations
{
    /// <inheritdoc />
    public partial class RenameCaldwellObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 110 });

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 14 },
                column: "Common",
                value: "Double Cluster");

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 25 },
                column: "Common",
                value: "Intergalactic Wanderer");

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 26 },
                column: "Common",
                value: "Silver Needle Galaxy");

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 43 },
                column: "Common",
                value: "Little Sombrero Galaxy");

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 44 },
                column: "Common",
                value: "Propeller Galaxy");

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 62 },
                column: "Common",
                value: "Needle's Eye Galaxy");

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 74 },
                column: "Common",
                value: "Southern Ring Nebula,Eight Burst Nebula");

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 103 },
                column: "Common",
                value: "Tarantula Nebula,30 Doradus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 14 },
                column: "Common",
                value: "Double Cluster (West)");

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 25 },
                column: "Common",
                value: null);

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 26 },
                column: "Common",
                value: null);

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 43 },
                column: "Common",
                value: null);

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 44 },
                column: "Common",
                value: null);

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 62 },
                column: "Common",
                value: null);

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 74 },
                column: "Common",
                value: "Eight Burst Nebula,Southern Ring Nebula");

            migrationBuilder.UpdateData(
                table: "DsoItems",
                keyColumns: new[] { "CatalogName", "Id" },
                keyValues: new object[] { "Caldwell", 103 },
                column: "Common",
                value: "Center of the Tarantula Nebula");

            migrationBuilder.InsertData(
                table: "DsoItems",
                columns: new[] { "CatalogName", "Id", "Common", "ConstellationName", "Declination", "Description", "Distance", "Magnitude", "RightAscension", "Type" },
                values: new object[] { "Caldwell", 110, "Double Cluster (East)", "Perseus", 57.133333299999997, "Open cluster", 7.5999999999999996, 6.0999999999999996, 2.3666667000000001, "Star cluster" });
        }
    }
}
