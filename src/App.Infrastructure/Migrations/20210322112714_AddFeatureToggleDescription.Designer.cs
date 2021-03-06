﻿// <auto-generated />
using System;
using App.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210322112714_AddFeatureToggleDescription")]
    partial class AddFeatureToggleDescription
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("App.Domain.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .HasColumnType("text");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("App.Domain.ActivityLog", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("InitiatedById")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("InitiatedById");

                    b.ToTable("ActivityLog");
                });

            modelBuilder.Entity("App.Domain.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("App.Domain.ApplicationAccessKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Secret")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId")
                        .IsUnique();

                    b.ToTable("ApplicationAccessKey");
                });

            modelBuilder.Entity("App.Domain.ApplicationEnvironment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastUpdateAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("ApplicationEnvironment");
                });

            modelBuilder.Entity("App.Domain.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ExternalCustomerId")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("App.Domain.FeatureToggles.FeatureToggle", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Key")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("FeatureToggle");
                });

            modelBuilder.Entity("App.Domain.FeatureToggles.FeatureToggleValue", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("EnvironmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FeatureToggleId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastReadAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId");

                    b.HasIndex("FeatureToggleId");

                    b.ToTable("FeatureToggleValue");
                });

            modelBuilder.Entity("App.Domain.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Permission")
                        .HasColumnType("integer");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Invitation");
                });

            modelBuilder.Entity("App.Domain.TargetRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserTargetingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserTargetingId")
                        .IsUnique();

                    b.ToTable("TargetRule");

                    b.HasDiscriminator<string>("Discriminator").HasValue("TargetRule");
                });

            modelBuilder.Entity("App.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("App.Domain.UserApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Permission")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserApplication");
                });

            modelBuilder.Entity("App.Domain.UserTargeting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EnvironmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FeatureToggleId")
                        .HasColumnType("uuid");

                    b.Property<int>("TargetId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId");

                    b.HasIndex("FeatureToggleId");

                    b.ToTable("UserTargeting");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("FriendlyName")
                        .HasColumnType("text");

                    b.Property<string>("Xml")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DataProtectionKeys");
                });

            modelBuilder.Entity("App.Domain.TargetPercentage", b =>
                {
                    b.HasBaseType("App.Domain.TargetRule");

                    b.HasDiscriminator().HasValue("TargetPercentage");
                });

            modelBuilder.Entity("App.Domain.TargetSpecificUsers", b =>
                {
                    b.HasBaseType("App.Domain.TargetRule");

                    b.Property<int>("Option")
                        .HasColumnType("integer");

                    b.Property<string>("Property")
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("TargetSpecificUsers");
                });

            modelBuilder.Entity("App.Domain.ActivityLog", b =>
                {
                    b.HasOne("App.Domain.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.User", "InitiatedBy")
                        .WithMany()
                        .HasForeignKey("InitiatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("InitiatedBy");
                });

            modelBuilder.Entity("App.Domain.ApplicationAccessKey", b =>
                {
                    b.HasOne("App.Domain.Application", "Application")
                        .WithOne("AccessKey")
                        .HasForeignKey("App.Domain.ApplicationAccessKey", "ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("App.Domain.ApplicationEnvironment", b =>
                {
                    b.HasOne("App.Domain.Application", "Application")
                        .WithMany("Environments")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("App.Domain.Customer", b =>
                {
                    b.HasOne("App.Domain.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("App.Domain.Customer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("App.Domain.FeatureToggles.FeatureToggle", b =>
                {
                    b.HasOne("App.Domain.Application", "Application")
                        .WithMany("FeatureToggles")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("App.Domain.FeatureToggles.FeatureToggleValue", b =>
                {
                    b.HasOne("App.Domain.ApplicationEnvironment", "Environment")
                        .WithMany("FeatureToggleValues")
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.FeatureToggles.FeatureToggle", "FeatureToggle")
                        .WithMany()
                        .HasForeignKey("FeatureToggleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Environment");

                    b.Navigation("FeatureToggle");
                });

            modelBuilder.Entity("App.Domain.Invitation", b =>
                {
                    b.HasOne("App.Domain.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("App.Domain.TargetRule", b =>
                {
                    b.HasOne("App.Domain.UserTargeting", "UserTargeting")
                        .WithOne("TargetRule")
                        .HasForeignKey("App.Domain.TargetRule", "UserTargetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserTargeting");
                });

            modelBuilder.Entity("App.Domain.User", b =>
                {
                    b.HasOne("App.Domain.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("App.Domain.UserApplication", b =>
                {
                    b.HasOne("App.Domain.Application", "Application")
                        .WithMany("Users")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("User");
                });

            modelBuilder.Entity("App.Domain.UserTargeting", b =>
                {
                    b.HasOne("App.Domain.ApplicationEnvironment", "Environment")
                        .WithMany()
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.FeatureToggles.FeatureToggle", "FeatureToggle")
                        .WithMany()
                        .HasForeignKey("FeatureToggleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Environment");

                    b.Navigation("FeatureToggle");
                });

            modelBuilder.Entity("App.Domain.TargetPercentage", b =>
                {
                    b.OwnsOne("App.Domain.Percent", "Percent", b1 =>
                        {
                            b1.Property<int>("TargetPercentageId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<int>("Number")
                                .HasColumnType("integer")
                                .HasColumnName("Percent");

                            b1.HasKey("TargetPercentageId");

                            b1.ToTable("TargetRule");

                            b1.WithOwner()
                                .HasForeignKey("TargetPercentageId");
                        });

                    b.Navigation("Percent");
                });

            modelBuilder.Entity("App.Domain.TargetSpecificUsers", b =>
                {
                    b.OwnsOne("App.Domain.ValuesList", "ValuesList", b1 =>
                        {
                            b1.Property<int>("TargetSpecificUsersId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<string>("List")
                                .HasColumnType("text")
                                .HasColumnName("ValueList");

                            b1.HasKey("TargetSpecificUsersId");

                            b1.ToTable("TargetRule");

                            b1.WithOwner()
                                .HasForeignKey("TargetSpecificUsersId");
                        });

                    b.Navigation("ValuesList");
                });

            modelBuilder.Entity("App.Domain.Application", b =>
                {
                    b.Navigation("AccessKey");

                    b.Navigation("Environments");

                    b.Navigation("FeatureToggles");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("App.Domain.ApplicationEnvironment", b =>
                {
                    b.Navigation("FeatureToggleValues");
                });

            modelBuilder.Entity("App.Domain.User", b =>
                {
                    b.Navigation("Customer");
                });

            modelBuilder.Entity("App.Domain.UserTargeting", b =>
                {
                    b.Navigation("TargetRule");
                });
#pragma warning restore 612, 618
        }
    }
}
