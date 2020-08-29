﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TournamentRecordKeeperApi.Data;

namespace TournamentRecordKeeperApi.Migrations
{
    [DbContext(typeof(appContext))]
    partial class appContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxPlayerCount")
                        .HasColumnType("int");

                    b.Property<int>("MinPlayerCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.GameMatch", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("MatchDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("gameID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("gameID");

                    b.ToTable("GameMatches");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.GameMode", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WinCondition")
                        .HasColumnType("int");

                    b.Property<int?>("gameID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("gameID");

                    b.ToTable("GameModes");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.Tournament", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TournamentType")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.TournamentType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("tournamentTypes");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.GameMatch", b =>
                {
                    b.HasOne("TournamentRecordKeeperApi.Models.Game", "game")
                        .WithMany()
                        .HasForeignKey("gameID");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.GameMode", b =>
                {
                    b.HasOne("TournamentRecordKeeperApi.Models.Game", "game")
                        .WithMany()
                        .HasForeignKey("gameID");
                });
#pragma warning restore 612, 618
        }
    }
}
