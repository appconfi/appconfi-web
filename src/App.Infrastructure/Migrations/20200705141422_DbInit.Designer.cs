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
    [Migration("20200705141422_DbInit")]
    partial class DbInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

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

            modelBuilder.Entity("App.Domain.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

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
                        .ValueGeneratedOnAdd()
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

            modelBuilder.Entity("App.Domain.ApplicationFeatureToggle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Key")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("ApplicationFeatureToggle");
                });

            modelBuilder.Entity("App.Domain.ApplicationFeatureToggleValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationEnvironmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationFeatureToggleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastReadAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationEnvironmentId");

                    b.HasIndex("ApplicationFeatureToggleId");

                    b.ToTable("ApplicationFeatureToggleValue");
                });

            modelBuilder.Entity("App.Domain.ApplicationPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<bool>("DefaultPlan")
                        .HasColumnType("boolean");

                    b.Property<string>("ExternalId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("NumberOfApplicationsAllowed")
                        .HasColumnType("integer");

                    b.Property<int?>("NumberOfEnvironmentsAllowed")
                        .HasColumnType("integer");

                    b.Property<int?>("NumberOfMembersAllowed")
                        .HasColumnType("integer");

                    b.Property<int?>("NumberOfTargetRulesAllowed")
                        .HasColumnType("integer");

                    b.Property<int>("SubscriptionDurationMonth")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ApplicationPlan");
                });

            modelBuilder.Entity("App.Domain.ApplicationSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Key")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("ApplicationSetting");
                });

            modelBuilder.Entity("App.Domain.ApplicationSettingValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationEnvironmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationSettingId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastReadAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Value")
                        .HasColumnType("character varying(2000)")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationEnvironmentId");

                    b.HasIndex("ApplicationSettingId");

                    b.ToTable("ApplicationSettingValue");
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
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

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
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("App.Domain.UserApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

            modelBuilder.Entity("App.Domain.UserSubscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("AutoRenovate")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ExternalId")
                        .HasColumnType("text");

                    b.Property<int>("PlanId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ValidUntilUtc")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSubscription");
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

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

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
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

                    b.ToTable("TargetRule1");

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

            modelBuilder.Entity("App.Domain.ApplicationAccessKey", b =>
                {
                    b.HasOne("App.Domain.Application", "Application")
                        .WithOne("AccessKey")
                        .HasForeignKey("App.Domain.ApplicationAccessKey", "ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.ApplicationEnvironment", b =>
                {
                    b.HasOne("App.Domain.Application", "Application")
                        .WithMany("Environments")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.ApplicationFeatureToggle", b =>
                {
                    b.HasOne("App.Domain.Application", "Application")
                        .WithMany("FeatureToggles")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.ApplicationFeatureToggleValue", b =>
                {
                    b.HasOne("App.Domain.ApplicationEnvironment", "Environment")
                        .WithMany("ApplicationFeatureToggleValues")
                        .HasForeignKey("ApplicationEnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.ApplicationFeatureToggle", "FeatureToggle")
                        .WithMany()
                        .HasForeignKey("ApplicationFeatureToggleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.ApplicationPlan", b =>
                {
                    b.OwnsOne("App.Domain.Money", "Price", b1 =>
                        {
                            b1.Property<int>("ApplicationPlanId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<long>("Amount")
                                .HasColumnName("PriceAmount")
                                .HasColumnType("bigint");

                            b1.Property<string>("Currency")
                                .HasColumnName("PriceCurrency")
                                .HasColumnType("text");

                            b1.HasKey("ApplicationPlanId");

                            b1.ToTable("ApplicationPlan");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationPlanId");
                        });
                });

            modelBuilder.Entity("App.Domain.ApplicationSetting", b =>
                {
                    b.HasOne("App.Domain.Application", "Application")
                        .WithMany("Settings")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.ApplicationSettingValue", b =>
                {
                    b.HasOne("App.Domain.ApplicationEnvironment", "Environment")
                        .WithMany("ApplicationSettingValues")
                        .HasForeignKey("ApplicationEnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.ApplicationSetting", "Setting")
                        .WithMany()
                        .HasForeignKey("ApplicationSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.Customer", b =>
                {
                    b.HasOne("App.Domain.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("App.Domain.Customer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.Invitation", b =>
                {
                    b.HasOne("App.Domain.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.TargetRule", b =>
                {
                    b.HasOne("App.Domain.UserTargeting", "UserTargeting")
                        .WithOne("TargetRule")
                        .HasForeignKey("App.Domain.TargetRule", "UserTargetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.User", b =>
                {
                    b.HasOne("App.Domain.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");
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
                });

            modelBuilder.Entity("App.Domain.UserSubscription", b =>
                {
                    b.HasOne("App.Domain.ApplicationPlan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.UserTargeting", b =>
                {
                    b.HasOne("App.Domain.ApplicationEnvironment", "Environment")
                        .WithMany()
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.ApplicationFeatureToggle", "FeatureToggle")
                        .WithMany()
                        .HasForeignKey("FeatureToggleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.TargetPercentage", b =>
                {
                    b.OwnsOne("App.Domain.Percent", "Percent", b1 =>
                        {
                            b1.Property<int>("TargetPercentageId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<int>("Number")
                                .HasColumnName("Percent")
                                .HasColumnType("integer");

                            b1.HasKey("TargetPercentageId");

                            b1.ToTable("TargetRule");

                            b1.WithOwner()
                                .HasForeignKey("TargetPercentageId");
                        });
                });

            modelBuilder.Entity("App.Domain.TargetSpecificUsers", b =>
                {
                    b.OwnsOne("App.Domain.ValuesList", "ValuesList", b1 =>
                        {
                            b1.Property<int>("TargetSpecificUsersId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<string>("List")
                                .HasColumnName("ValueList")
                                .HasColumnType("text");

                            b1.HasKey("TargetSpecificUsersId");

                            b1.ToTable("TargetRule");

                            b1.WithOwner()
                                .HasForeignKey("TargetSpecificUsersId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
