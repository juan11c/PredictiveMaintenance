using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PredictiveMaintenance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSensorEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorData_Machines_MachineId",
                table: "SensorData");

            migrationBuilder.DropColumn(
                name: "EnergyConsumption",
                table: "SensorData");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "SensorData");

            migrationBuilder.RenameColumn(
                name: "Vibration",
                table: "SensorData",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "MachineId",
                table: "SensorData",
                newName: "SensorId");

            migrationBuilder.RenameIndex(
                name: "IX_SensorData_MachineId",
                table: "SensorData",
                newName: "IX_SensorData_SensorId");

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_MachineId",
                table: "Sensors",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorData_Sensors_SensorId",
                table: "SensorData",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorData_Sensors_SensorId",
                table: "SensorData");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "SensorData",
                newName: "Vibration");

            migrationBuilder.RenameColumn(
                name: "SensorId",
                table: "SensorData",
                newName: "MachineId");

            migrationBuilder.RenameIndex(
                name: "IX_SensorData_SensorId",
                table: "SensorData",
                newName: "IX_SensorData_MachineId");

            migrationBuilder.AddColumn<double>(
                name: "EnergyConsumption",
                table: "SensorData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "SensorData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_SensorData_Machines_MachineId",
                table: "SensorData",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
