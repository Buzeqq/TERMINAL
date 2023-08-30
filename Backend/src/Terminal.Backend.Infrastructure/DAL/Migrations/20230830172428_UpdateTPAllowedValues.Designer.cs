﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Terminal.Backend.Infrastructure.DAL;

#nullable disable

namespace Terminal.Backend.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(TerminalDbContext))]
    [Migration("20230830172428_UpdateTPAllowedValues")]
    partial class UpdateTPAllowedValues
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MeasurementTag", b =>
                {
                    b.Property<Guid>("MeasurementId")
                        .HasColumnType("uuid");

                    b.Property<string>("TagsName")
                        .HasColumnType("text");

                    b.HasKey("MeasurementId", "TagsName");

                    b.HasIndex("TagsName");

                    b.ToTable("MeasurementTag");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Measurement", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.ParameterValues.ParameterValue", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ParameterName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("StepId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParameterName");

                    b.HasIndex("StepId");

                    b.ToTable("ParameterValues");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ParameterValue");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Parameters.Parameter", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("Parameters");

                    b.HasDiscriminator<string>("Type").HasValue("Parameter");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Step", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("MeasurementId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RecipeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Steps");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Tag", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("Name");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.ParameterValues.DecimalParameterValue", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.ParameterValues.ParameterValue");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric")
                        .HasColumnName("DecimalParameterValue_Value");

                    b.HasDiscriminator().HasValue("DecimalParameterValue");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.ParameterValues.IntegerParameterValue", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.ParameterValues.ParameterValue");

                    b.Property<int>("Value")
                        .HasColumnType("integer")
                        .HasColumnName("IntegerParameterValue_Value");

                    b.HasDiscriminator().HasValue("IntegerParameterValue");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.ParameterValues.TextParameterValue", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.ParameterValues.ParameterValue");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("TextParameterValue_Value");

                    b.HasDiscriminator().HasValue("TextParameterValue");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Parameters.NumericParameter", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.Parameters.Parameter");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NumericParameter_Unit");

                    b.HasDiscriminator().HasValue("NumericParameter");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Parameters.TextParameter", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.Parameters.Parameter");

                    b.Property<List<string>>("AllowedValues")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("TextParameter_AllowedValues");

                    b.HasDiscriminator().HasValue("TextParameter");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Parameters.DecimalParameter", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.Parameters.NumericParameter");

                    b.Property<decimal>("Step")
                        .HasColumnType("numeric")
                        .HasColumnName("DecimalParameter_Step");

                    b.HasDiscriminator().HasValue("DecimalParameter");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Parameters.IntegerParameter", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.Parameters.NumericParameter");

                    b.Property<int>("Step")
                        .HasColumnType("integer")
                        .HasColumnName("IntegerParameter_Step");

                    b.HasDiscriminator().HasValue("IntegerParameter");
                });

            modelBuilder.Entity("MeasurementTag", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Measurement", null)
                        .WithMany()
                        .HasForeignKey("MeasurementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Terminal.Backend.Core.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Measurement", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Project", null)
                        .WithMany("Measurements")
                        .HasForeignKey("ProjectId");

                    b.HasOne("Terminal.Backend.Core.Entities.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.ParameterValues.ParameterValue", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Parameters.Parameter", "Parameter")
                        .WithMany()
                        .HasForeignKey("ParameterName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Terminal.Backend.Core.Entities.Step", null)
                        .WithMany("Parameters")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parameter");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Step", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Measurement", null)
                        .WithMany("Steps")
                        .HasForeignKey("MeasurementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Terminal.Backend.Core.Entities.Recipe", null)
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Measurement", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Project", b =>
                {
                    b.Navigation("Measurements");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Recipe", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Step", b =>
                {
                    b.Navigation("Parameters");
                });
#pragma warning restore 612, 618
        }
    }
}
