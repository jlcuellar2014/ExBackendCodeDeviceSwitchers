﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SwitchTesterApi.Models.Contexts;

#nullable disable

namespace SwitchTesterApi.Migrations
{
    [DbContext(typeof(SwitchTesterContext))]
    [Migration("20231130160105_InitialSchema")]
    partial class InitialSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("SwitchTesterApi.Models.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DeviceId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("SwitchTesterApi.Models.DevicePort", b =>
                {
                    b.Property<int>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.HasKey("DeviceId", "Port");

                    b.ToTable("DevicePorts");
                });

            modelBuilder.Entity("SwitchTesterApi.Models.DeviceSwitchConnection", b =>
                {
                    b.Property<int>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SwitchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.HasKey("DeviceId", "SwitchId", "Port");

                    b.HasIndex("SwitchId");

                    b.ToTable("DeviceSwitchConnections");
                });

            modelBuilder.Entity("SwitchTesterApi.Models.Switch", b =>
                {
                    b.Property<int>("SwitchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SwitchId");

                    b.ToTable("Switches");
                });

            modelBuilder.Entity("SwitchTesterApi.Models.SwitchPort", b =>
                {
                    b.Property<int>("SwitchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.HasKey("SwitchId", "Port");

                    b.ToTable("SwitchPorts");
                });

            modelBuilder.Entity("SwitchTesterApi.Models.DevicePort", b =>
                {
                    b.HasOne("SwitchTesterApi.Models.Device", "Device")
                        .WithMany("Ports")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("SwitchTesterApi.Models.DeviceSwitchConnection", b =>
                {
                    b.HasOne("SwitchTesterApi.Models.Device", "Device")
                        .WithMany("Connections")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SwitchTesterApi.Models.Switch", "Switch")
                        .WithMany("Connections")
                        .HasForeignKey("SwitchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Switch");
                });

            modelBuilder.Entity("SwitchTesterApi.Models.SwitchPort", b =>
                {
                    b.HasOne("SwitchTesterApi.Models.Switch", "Switch")
                        .WithMany("Ports")
                        .HasForeignKey("SwitchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Switch");
                });

            modelBuilder.Entity("SwitchTesterApi.Models.Device", b =>
                {
                    b.Navigation("Connections");

                    b.Navigation("Ports");
                });

            modelBuilder.Entity("SwitchTesterApi.Models.Switch", b =>
                {
                    b.Navigation("Connections");

                    b.Navigation("Ports");
                });
#pragma warning restore 612, 618
        }
    }
}
