﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Terminal.Backend.Infrastructure.DAL;

#nullable disable

namespace Terminal.Backend.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(TerminalDbContext))]
    partial class TerminalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MeasurementTag", b =>
                {
                    b.Property<Guid>("MeasurementId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uuid");

                    b.HasKey("MeasurementId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("MeasurementTag");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpiresIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Measurement", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<ulong>("Code")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("Code", "Comment")
                        .HasAnnotation("Npgsql:TsVectorConfig", "english");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("Code", "Comment"), "GIN");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.ParameterValues.ParameterValue", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ParameterName")
                        .HasColumnType("uuid");

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
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Parameters");

                    b.HasDiscriminator<string>("Type").HasValue("Parameter");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "UserRead"
                        },
                        new
                        {
                            Id = 2,
                            Name = "UserWrite"
                        },
                        new
                        {
                            Id = 3,
                            Name = "UserUpdate"
                        },
                        new
                        {
                            Id = 4,
                            Name = "UserDelete"
                        },
                        new
                        {
                            Id = 5,
                            Name = "ProjectRead"
                        },
                        new
                        {
                            Id = 6,
                            Name = "ProjectWrite"
                        },
                        new
                        {
                            Id = 7,
                            Name = "ProjectUpdate"
                        },
                        new
                        {
                            Id = 8,
                            Name = "ProjectDelete"
                        },
                        new
                        {
                            Id = 9,
                            Name = "RecipeRead"
                        },
                        new
                        {
                            Id = 10,
                            Name = "RecipeWrite"
                        },
                        new
                        {
                            Id = 11,
                            Name = "RecipeUpdate"
                        },
                        new
                        {
                            Id = 12,
                            Name = "RecipeDelete"
                        },
                        new
                        {
                            Id = 13,
                            Name = "TagRead"
                        },
                        new
                        {
                            Id = 14,
                            Name = "TagWrite"
                        },
                        new
                        {
                            Id = 15,
                            Name = "TagUpdate"
                        },
                        new
                        {
                            Id = 16,
                            Name = "TagDelete"
                        },
                        new
                        {
                            Id = 17,
                            Name = "MeasurementRead"
                        },
                        new
                        {
                            Id = 18,
                            Name = "MeasurementWrite"
                        },
                        new
                        {
                            Id = 19,
                            Name = "MeasurementUpdate"
                        },
                        new
                        {
                            Id = 20,
                            Name = "MeasurementDelete"
                        },
                        new
                        {
                            Id = 21,
                            Name = "ParameterRead"
                        },
                        new
                        {
                            Id = 22,
                            Name = "ParameterWrite"
                        },
                        new
                        {
                            Id = 23,
                            Name = "ParameterUpdate"
                        },
                        new
                        {
                            Id = 24,
                            Name = "ParameterDelete"
                        },
                        new
                        {
                            Id = 25,
                            Name = "StepRead"
                        },
                        new
                        {
                            Id = 26,
                            Name = "StepWrite"
                        },
                        new
                        {
                            Id = 27,
                            Name = "StepUpdate"
                        },
                        new
                        {
                            Id = 28,
                            Name = "StepDelete"
                        });
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

                    b.HasIndex("RecipeName")
                        .HasAnnotation("Npgsql:TsVectorConfig", "english");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("RecipeName"), "GIN");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Role", b =>
                {
                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Value");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Value = 3,
                            Name = "Administrator"
                        },
                        new
                        {
                            Value = 2,
                            Name = "Moderator"
                        },
                        new
                        {
                            Value = 1,
                            Name = "Registered"
                        },
                        new
                        {
                            Value = 0,
                            Name = "Guest"
                        });
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.RolePermission", b =>
                {
                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission");

                    b.HasData(
                        new
                        {
                            PermissionId = 1,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 2,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 3,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 4,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 5,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 6,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 7,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 8,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 9,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 10,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 11,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 12,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 13,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 14,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 15,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 16,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 17,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 18,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 19,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 20,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 21,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 22,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 23,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 24,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 25,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 26,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 27,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 28,
                            RoleId = 3
                        },
                        new
                        {
                            PermissionId = 1,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 5,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 6,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 7,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 8,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 9,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 10,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 11,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 12,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 13,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 14,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 15,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 16,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 17,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 18,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 19,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 20,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 21,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 22,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 23,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 24,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 25,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 26,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 27,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 28,
                            RoleId = 2
                        },
                        new
                        {
                            PermissionId = 5,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 9,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 13,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 14,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 15,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 17,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 18,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 19,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 21,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 25,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 26,
                            RoleId = 1
                        },
                        new
                        {
                            PermissionId = 27,
                            RoleId = 1
                        });
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

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Steps");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Tag", b =>
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

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("Activated")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleValue")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleValue");

                    b.ToTable("Users");
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
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Invitation", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Measurement", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Project", "Project")
                        .WithMany("Measurements")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Terminal.Backend.Core.Entities.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId");

                    b.Navigation("Project");

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

            modelBuilder.Entity("Terminal.Backend.Core.Entities.RolePermission", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Terminal.Backend.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Step", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Measurement", null)
                        .WithMany("Steps")
                        .HasForeignKey("MeasurementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Terminal.Backend.Core.Entities.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.User", b =>
                {
                    b.HasOne("Terminal.Backend.Core.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleValue")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
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

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Terminal.Backend.Core.Entities.Step", b =>
                {
                    b.Navigation("Parameters");
                });
#pragma warning restore 612, 618
        }
    }
}
