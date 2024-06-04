﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Terminal.Backend.Infrastructure.DAL;

#nullable disable

namespace Terminal.Backend.Infrastructure.DAL.Migrations.Data
{
    [DbContext(typeof(TerminalDbContext))]
    partial class TerminalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("data")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SampleTag", b =>
                {
                    b.Property<Guid>("SampleId")
                        .HasColumnType("uuid")
                        .HasColumnName("sample_id");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uuid")
                        .HasColumnName("tags_id");

                    b.HasKey("SampleId", "TagsId")
                        .HasName("pk_sample_tag");

                    b.HasIndex("TagsId")
                        .HasDatabaseName("ix_sample_tag_tags_id");

                    b.ToTable("sample_tag", "data");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.ParameterValues.ParameterValue", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("StepId")
                        .HasColumnType("uuid")
                        .HasColumnName("step_id");

                    b.Property<Guid>("parameter_name")
                        .HasColumnType("uuid")
                        .HasColumnName("parameter_name");

                    b.Property<string>("parameter_type")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)")
                        .HasColumnName("parameter_type");

                    b.HasKey("Id")
                        .HasName("pk_parameter_values");

                    b.HasIndex("StepId")
                        .HasDatabaseName("ix_parameter_values_step_id");

                    b.HasIndex("parameter_name")
                        .HasDatabaseName("ix_parameter_values_parameter_name");

                    b.ToTable("parameter_values", "data");

                    b.HasDiscriminator<string>("parameter_type").HasValue("ParameterValue");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Parameters.Parameter", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long>("Order")
                        .HasColumnType("bigint")
                        .HasColumnName("order");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid")
                        .HasColumnName("parent_id");

                    b.HasKey("Id")
                        .HasName("pk_parameters");

                    b.HasIndex("ParentId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_projects");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_projects_name");

                    b.ToTable("projects", "data");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("recipe_name");

                    b.HasKey("Id")
                        .HasName("pk_recipes");

                    b.HasIndex("RecipeName")
                        .HasDatabaseName("ix_recipes_recipe_name")
                        .HasAnnotation("Npgsql:TsVectorConfig", "english");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("RecipeName"), "GIN");

                    b.ToTable("recipes", "data");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Sample", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<ulong>("Code")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("code");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid")
                        .HasColumnName("project_id");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid")
                        .HasColumnName("recipe_id");

                    b.HasKey("Id")
                        .HasName("pk_samples");

                    b.HasIndex("ProjectId")
                        .HasDatabaseName("ix_samples_project_id");

                    b.HasIndex("RecipeId")
                        .HasDatabaseName("ix_samples_recipe_id");

                    b.HasIndex("Code", "Comment")
                        .HasDatabaseName("ix_samples_code_comment")
                        .HasAnnotation("Npgsql:TsVectorConfig", "english");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("Code", "Comment"), "GIN");

                    b.ToTable("samples", "data");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Step", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_tags");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_tags_name");

                    b.ToTable("tags", "data");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.ParameterValues.DecimalParameterValue", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.ParameterValues.ParameterValue");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric")
                        .HasColumnName("decimal_value");

                    b.HasDiscriminator().HasValue("decimal");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.ParameterValues.IntegerParameterValue", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.ParameterValues.ParameterValue");

                    b.Property<int>("Value")
                        .HasColumnType("integer")
                        .HasColumnName("integer_value");

                    b.HasDiscriminator().HasValue("integer");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.ParameterValues.TextParameterValue", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.ParameterValues.ParameterValue");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text_value");

                    b.HasDiscriminator().HasValue("text");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Parameters.NumericParameter", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.Parameters.Parameter");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("unit");

                    b.ToTable((string)null);
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Parameters.TextParameter", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.Parameters.Parameter");

                    b.Property<List<string>>("AllowedValues")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("allowed_values");

                    b.Property<long>("DefaultValue")
                        .HasColumnType("bigint")
                        .HasColumnName("default_value");

                    b.ToTable("text_parameters", "data");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.RecipeStep", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.Step");

                    b.Property<Guid>("RecipeId")
                        .HasColumnType("uuid")
                        .HasColumnName("recipe_id");

                    b.HasIndex("RecipeId")
                        .HasDatabaseName("ix_recipe_steps_recipe_id");

                    b.ToTable("recipe_steps", "data");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.SampleStep", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.Step");

                    b.Property<Guid>("SampleId")
                        .HasColumnType("uuid")
                        .HasColumnName("sample_id");

                    b.HasIndex("SampleId")
                        .HasDatabaseName("ix_sample_steps_sample_id");

                    b.ToTable("sample_steps", "data");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Parameters.DecimalParameter", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.Parameters.NumericParameter");

                    b.Property<decimal>("DefaultValue")
                        .HasColumnType("numeric")
                        .HasColumnName("default_value");

                    b.Property<decimal>("Step")
                        .HasColumnType("numeric")
                        .HasColumnName("step");

                    b.ToTable("decimal_parameters", "data");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Parameters.IntegerParameter", b =>
                {
                    b.HasBaseType("Terminal.Backend.Core.Entities.Parameters.NumericParameter");

                    b.Property<int>("DefaultValue")
                        .HasColumnType("integer")
                        .HasColumnName("default_value");

                    b.Property<int>("Step")
                        .HasColumnType("integer")
                        .HasColumnName("step");

                    b.ToTable("integer_parameters", "data");
                });

            modelBuilder.Entity("SampleTag", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Sample", null)
                        .WithMany()
                        .HasForeignKey("SampleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sample_tag_samples_sample_id");

                    b.HasOne("Terminal.Backend.Core.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sample_tag_tags_tags_id");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.ParameterValues.ParameterValue", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Step", null)
                        .WithMany("Parameters")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_parameter_values_step_step_id");

                    b.HasOne("Terminal.Backend.Core.Entities.Parameters.Parameter", "Parameter")
                        .WithMany()
                        .HasForeignKey("parameter_name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_parameter_values_parameters_parameter_name");

                    b.Navigation("Parameter");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Parameters.Parameter", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Parameters.Parameter", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Sample", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Project", "Project")
                        .WithMany("Samples")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_samples_projects_project_id");

                    b.HasOne("Terminal.Backend.Core.Entities.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_samples_recipes_recipe_id");

                    b.Navigation("Project");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.RecipeStep", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_recipe_steps_recipes_recipe_id");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.SampleStep", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Sample", null)
                        .WithMany("Steps")
                        .HasForeignKey("SampleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sample_steps_samples_sample_id");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Project", b =>
                {
                    b.Navigation("Samples");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Recipe", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Sample", b =>
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
