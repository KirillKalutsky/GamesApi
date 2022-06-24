﻿// <auto-generated />
using System;
using GamesApi.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GamesApi.Migrations
{
    [DbContext(typeof(GameContext))]
    partial class GameContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GamesApi.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int[]>("GameGenres")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("StudioDeveloperId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StudioDeveloperId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GamesApi.Models.StudioDeveloper", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FoundingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("GamesApi.Models.Game", b =>
                {
                    b.HasOne("GamesApi.Models.StudioDeveloper", "StudioDeveloper")
                        .WithMany("Games")
                        .HasForeignKey("StudioDeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudioDeveloper");
                });

            modelBuilder.Entity("GamesApi.Models.StudioDeveloper", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
