﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TournamentRecordKeeperApi.Data;

namespace TournamentRecordKeeperApi.Migrations
{
    [DbContext(typeof(appContext))]
    [Migration("20200923061007_Added FirstOf BestOf and Survivals tables")]
    partial class AddedFirstOfBestOfandSurvivalstables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.BestOf", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumberOfMatches")
                        .HasColumnType("int");

                    b.Property<int>("Win")
                        .HasColumnType("int");

                    b.Property<int?>("gameModeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("gameModeID");

                    b.ToTable("BestOf");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.FirstOf", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Threshold")
                        .HasColumnType("int");

                    b.Property<int?>("gameModeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("gameModeID");

                    b.ToTable("FirstOf");
                });

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

                    b.Property<int?>("tournamentID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("gameID");

                    b.HasIndex("tournamentID");

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

                    b.Property<int?>("gameID")
                        .HasColumnType("int");

                    b.Property<int?>("winConditionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("gameID");

                    b.HasIndex("winConditionID");

                    b.ToTable("GameModes");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.Survival", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Threshold")
                        .HasColumnType("int");

                    b.Property<int?>("gameModeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("gameModeID");

                    b.ToTable("Survivals");
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

                    b.Property<int?>("tournamentTypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("tournamentTypeID");

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

                    b.ToTable("TournamentTypes");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.WinCondition", b =>
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

                    b.ToTable("WinConditions");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.BestOf", b =>
                {
                    b.HasOne("TournamentRecordKeeperApi.Models.GameMode", "gameMode")
                        .WithMany()
                        .HasForeignKey("gameModeID");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.FirstOf", b =>
                {
                    b.HasOne("TournamentRecordKeeperApi.Models.GameMode", "gameMode")
                        .WithMany()
                        .HasForeignKey("gameModeID");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.GameMatch", b =>
                {
                    b.HasOne("TournamentRecordKeeperApi.Models.Game", "game")
                        .WithMany()
                        .HasForeignKey("gameID");

                    b.HasOne("TournamentRecordKeeperApi.Models.Tournament", "tournament")
                        .WithMany()
                        .HasForeignKey("tournamentID");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.GameMode", b =>
                {
                    b.HasOne("TournamentRecordKeeperApi.Models.Game", "game")
                        .WithMany("GameModes")
                        .HasForeignKey("gameID");

                    b.HasOne("TournamentRecordKeeperApi.Models.WinCondition", "winCondition")
                        .WithMany()
                        .HasForeignKey("winConditionID");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.Survival", b =>
                {
                    b.HasOne("TournamentRecordKeeperApi.Models.GameMode", "gameMode")
                        .WithMany()
                        .HasForeignKey("gameModeID");
                });

            modelBuilder.Entity("TournamentRecordKeeperApi.Models.Tournament", b =>
                {
                    b.HasOne("TournamentRecordKeeperApi.Models.TournamentType", "tournamentType")
                        .WithMany()
                        .HasForeignKey("tournamentTypeID");
                });
#pragma warning restore 612, 618
        }
    }
}
