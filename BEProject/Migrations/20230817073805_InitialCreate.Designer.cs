﻿// <auto-generated />
using BEProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BEProject.Migrations
{
    [DbContext(typeof(BEDbContext))]
    [Migration("20230817073805_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BEProject.Model.Title", b =>
                {
                    b.Property<int>("TitleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TitleId"));

                    b.Property<string>("TitleCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TitleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TitleId");

                    b.ToTable("Titles");
                });
#pragma warning restore 612, 618
        }
    }
}
