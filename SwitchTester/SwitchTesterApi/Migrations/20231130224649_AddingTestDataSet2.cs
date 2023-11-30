using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchTesterApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingTestDataSet2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "DevicePorts",
               columns: new[] { "DeviceId", "Port" },
               values: new object[,] {
                    {1, 6101},
                    {1, 6102},
                    {2, 6201},
                    {2, 6202},
               }
           );

           migrationBuilder.InsertData(
               table: "SwitchPorts",
               columns: new[] { "SwitchId", "Port" },
               values: new object[,] {
                    {1, 6101},
                    {1, 6102},
                    {2, 6201},
                    {2, 6202},
               }
           );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
