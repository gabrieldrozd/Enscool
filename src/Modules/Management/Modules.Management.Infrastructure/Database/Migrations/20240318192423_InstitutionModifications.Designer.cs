﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Management.Infrastructure.Database;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Modules.Management.Infrastructure.Database.Migrations
{
    [DbContext(typeof(ManagementDbContext))]
    [Migration("20240318192423_InstitutionModifications")]
    partial class InstitutionModifications
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Management")
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Modules.Management.Domain.Institutions.Institution", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<Guid?>("InstitutionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ShortName")
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.ToTable("Institutions", "Management");
                });

            modelBuilder.Entity("Modules.Management.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DeletedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("InstitutionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", "Management");
                });

            modelBuilder.Entity("Modules.Management.Domain.Institutions.Institution", b =>
                {
                    b.OwnsMany("Core.Domain.Shared.EntityIds.UserId", "AdministratorIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("InstitutionId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("AdministratorId");

                            b1.HasKey("Id");

                            b1.HasIndex("InstitutionId");

                            b1.ToTable("InstitutionAdministratorIds", "Management");

                            b1.WithOwner()
                                .HasForeignKey("InstitutionId");
                        });

                    b.OwnsOne("Core.Domain.Shared.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("InstitutionId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("State")
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("ZipCodeCity")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("InstitutionId");

                            b1.ToTable("Institutions", "Management");

                            b1.WithOwner()
                                .HasForeignKey("InstitutionId");
                        });

                    b.Navigation("Address");

                    b.Navigation("AdministratorIds");
                });

            modelBuilder.Entity("Modules.Management.Domain.Users.User", b =>
                {
                    b.OwnsMany("Modules.Management.Domain.Users.ActivationCode", "ActivationCodes", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<DateTimeOffset>("CreatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTimeOffset>("Expires")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<bool>("IsActive")
                                .HasColumnType("boolean");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("UserId", "Id");

                            b1.ToTable("UserActivationCodes", "Management");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("ActivationCodes");
                });
#pragma warning restore 612, 618
        }
    }
}