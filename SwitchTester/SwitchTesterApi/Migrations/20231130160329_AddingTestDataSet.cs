using Microsoft.EntityFrameworkCore.Migrations;
using SwitchTesterApi.Models;

#nullable disable

namespace SwitchTesterApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingTestDataSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceId", "HostName" },
                values: new object[,] {
                    {1, "Device 1"},
                    {2, "Device 2"},
                }
            );

            migrationBuilder.InsertData(
                table: "DevicePorts",
                columns: new[] { "DeviceId", "Port" },
                values: new object[,] {
                    {1, 5101},
                    {1, 5102},
                    {2, 5201},
                    {2, 5202},
                }
            );

            migrationBuilder.InsertData(
                table: "Switches",
                columns: new[] { "SwitchId", "HostName" },
                values: new object[,] {
                    {1, "Switch 1"},
                    {2, "Switch 2"},
                }
            );

            migrationBuilder.InsertData(
                table: "SwitchPorts",
                columns: new[] { "SwitchId", "Port" },
                values: new object[,] {
                    {1, 5101},
                    {1, 5102},
                    {2, 5201},
                    {2, 5202},
                }
            );

            migrationBuilder.InsertData(
                table: "DeviceSwitchConnections",
                columns: new[] { "SwitchId", "DeviceId","Port" },
                values: new object[,] {
                    {1, 1, 5101},
                    {1, 1, 5102},
                    {2, 2, 5201},
                    {2, 2, 5202},
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
