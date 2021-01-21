using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace App.Infrastructure.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    VerifiedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationPlan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    NumberOfApplicationsAllowed = table.Column<int>(nullable: true),
                    NumberOfMembersAllowed = table.Column<int>(nullable: true),
                    NumberOfEnvironmentsAllowed = table.Column<int>(nullable: true),
                    NumberOfTargetRulesAllowed = table.Column<int>(nullable: true),
                    PriceCurrency = table.Column<string>(nullable: true),
                    PriceAmount = table.Column<long>(nullable: true),
                    SubscriptionDurationMonth = table.Column<int>(nullable: false),
                    DefaultPlan = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ExternalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FriendlyName = table.Column<string>(nullable: true),
                    Xml = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    AccountId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationAccessKey",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    Secret = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationAccessKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationAccessKey_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationEnvironment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    LastUpdateAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationEnvironment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationEnvironment_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationFeatureToggle",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationFeatureToggle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationFeatureToggle_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationSetting_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Token = table.Column<string>(nullable: true),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Permission = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExternalCustomerId = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserApplication",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Permission = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserApplication_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserApplication_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    AutoRenovate = table.Column<bool>(nullable: false),
                    ValidUntilUtc = table.Column<DateTime>(nullable: true),
                    PlanId = table.Column<int>(nullable: false),
                    ExternalId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubscription_ApplicationPlan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "ApplicationPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubscription_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationFeatureToggleValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastReadAt = table.Column<DateTime>(nullable: true),
                    LastModifiedAt = table.Column<DateTime>(nullable: true),
                    ApplicationEnvironmentId = table.Column<Guid>(nullable: false),
                    ApplicationFeatureToggleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationFeatureToggleValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationFeatureToggleValue_ApplicationEnvironment_Applic~",
                        column: x => x.ApplicationEnvironmentId,
                        principalTable: "ApplicationEnvironment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationFeatureToggleValue_ApplicationFeatureToggle_Appl~",
                        column: x => x.ApplicationFeatureToggleId,
                        principalTable: "ApplicationFeatureToggle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTargeting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EnvironmentId = table.Column<Guid>(nullable: false),
                    FeatureToggleId = table.Column<Guid>(nullable: false),
                    TargetId = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTargeting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTargeting_ApplicationEnvironment_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "ApplicationEnvironment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTargeting_ApplicationFeatureToggle_FeatureToggleId",
                        column: x => x.FeatureToggleId,
                        principalTable: "ApplicationFeatureToggle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSettingValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastReadAt = table.Column<DateTime>(nullable: true),
                    LastModifiedAt = table.Column<DateTime>(nullable: true),
                    ApplicationEnvironmentId = table.Column<Guid>(nullable: false),
                    ApplicationSettingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettingValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationSettingValue_ApplicationEnvironment_ApplicationE~",
                        column: x => x.ApplicationEnvironmentId,
                        principalTable: "ApplicationEnvironment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationSettingValue_ApplicationSetting_ApplicationSetti~",
                        column: x => x.ApplicationSettingId,
                        principalTable: "ApplicationSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserTargetingId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Percent = table.Column<int>(nullable: true),
                    ValueList = table.Column<string>(nullable: true),
                    Option = table.Column<int>(nullable: true),
                    Property = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TargetRule_UserTargeting_UserTargetingId",
                        column: x => x.UserTargetingId,
                        principalTable: "UserTargeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationAccessKey_ApplicationId",
                table: "ApplicationAccessKey",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationEnvironment_ApplicationId",
                table: "ApplicationEnvironment",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFeatureToggle_ApplicationId",
                table: "ApplicationFeatureToggle",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFeatureToggleValue_ApplicationEnvironmentId",
                table: "ApplicationFeatureToggleValue",
                column: "ApplicationEnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFeatureToggleValue_ApplicationFeatureToggleId",
                table: "ApplicationFeatureToggleValue",
                column: "ApplicationFeatureToggleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSetting_ApplicationId",
                table: "ApplicationSetting",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSettingValue_ApplicationEnvironmentId",
                table: "ApplicationSettingValue",
                column: "ApplicationEnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSettingValue_ApplicationSettingId",
                table: "ApplicationSettingValue",
                column: "ApplicationSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_ApplicationId",
                table: "Invitation",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetRule_UserTargetingId",
                table: "TargetRule",
                column: "UserTargetingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_AccountId",
                table: "User",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplication_ApplicationId",
                table: "UserApplication",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplication_UserId",
                table: "UserApplication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_PlanId",
                table: "UserSubscription",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_UserId",
                table: "UserSubscription",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTargeting_EnvironmentId",
                table: "UserTargeting",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTargeting_FeatureToggleId",
                table: "UserTargeting",
                column: "FeatureToggleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationAccessKey");

            migrationBuilder.DropTable(
                name: "ApplicationFeatureToggleValue");

            migrationBuilder.DropTable(
                name: "ApplicationSettingValue");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "DataProtectionKeys");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "TargetRule");

            migrationBuilder.DropTable(
                name: "UserApplication");

            migrationBuilder.DropTable(
                name: "UserSubscription");

            migrationBuilder.DropTable(
                name: "ApplicationSetting");

            migrationBuilder.DropTable(
                name: "UserTargeting");

            migrationBuilder.DropTable(
                name: "ApplicationPlan");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ApplicationEnvironment");

            migrationBuilder.DropTable(
                name: "ApplicationFeatureToggle");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Application");
        }
    }
}
