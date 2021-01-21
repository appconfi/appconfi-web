using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace App.Infrastructure.Migrations
{
    public partial class RemoveApplicationSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTargeting_ApplicationFeatureToggle_FeatureToggleId",
                table: "UserTargeting");

            migrationBuilder.DropTable(
                name: "ApplicationFeatureToggleValue");

            migrationBuilder.DropTable(
                name: "ApplicationSettingValue");

            migrationBuilder.DropTable(
                name: "UserSubscription");

            migrationBuilder.DropTable(
                name: "ApplicationFeatureToggle");

            migrationBuilder.DropTable(
                name: "ApplicationSetting");

            migrationBuilder.DropTable(
                name: "ApplicationPlan");

            migrationBuilder.CreateTable(
                name: "FeatureToggle",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureToggle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureToggle_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureToggleValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastReadAt = table.Column<DateTime>(nullable: true),
                    LastModifiedAt = table.Column<DateTime>(nullable: true),
                    EnvironmentId = table.Column<Guid>(nullable: false),
                    FeatureToggleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureToggleValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureToggleValue_ApplicationEnvironment_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "ApplicationEnvironment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureToggleValue_FeatureToggle_FeatureToggleId",
                        column: x => x.FeatureToggleId,
                        principalTable: "FeatureToggle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureToggle_ApplicationId",
                table: "FeatureToggle",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureToggleValue_EnvironmentId",
                table: "FeatureToggleValue",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureToggleValue_FeatureToggleId",
                table: "FeatureToggleValue",
                column: "FeatureToggleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTargeting_FeatureToggle_FeatureToggleId",
                table: "UserTargeting",
                column: "FeatureToggleId",
                principalTable: "FeatureToggle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTargeting_FeatureToggle_FeatureToggleId",
                table: "UserTargeting");

            migrationBuilder.DropTable(
                name: "FeatureToggleValue");

            migrationBuilder.DropTable(
                name: "FeatureToggle");

            migrationBuilder.CreateTable(
                name: "ApplicationFeatureToggle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Key = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
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
                name: "ApplicationPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    DefaultPlan = table.Column<bool>(type: "boolean", nullable: false),
                    ExternalId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NumberOfApplicationsAllowed = table.Column<int>(type: "integer", nullable: true),
                    NumberOfEnvironmentsAllowed = table.Column<int>(type: "integer", nullable: true),
                    NumberOfMembersAllowed = table.Column<int>(type: "integer", nullable: true),
                    NumberOfTargetRulesAllowed = table.Column<int>(type: "integer", nullable: true),
                    SubscriptionDurationMonth = table.Column<int>(type: "integer", nullable: false),
                    PriceAmount = table.Column<long>(type: "bigint", nullable: true),
                    PriceCurrency = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Key = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
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
                name: "ApplicationFeatureToggleValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationEnvironmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationFeatureToggleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastReadAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                name: "UserSubscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AutoRenovate = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExternalId = table.Column<string>(type: "text", nullable: true),
                    PlanId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValidUntilUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                name: "ApplicationSettingValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationEnvironmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationSettingId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastReadAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Value = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
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
                name: "IX_UserSubscription_PlanId",
                table: "UserSubscription",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_UserId",
                table: "UserSubscription",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTargeting_ApplicationFeatureToggle_FeatureToggleId",
                table: "UserTargeting",
                column: "FeatureToggleId",
                principalTable: "ApplicationFeatureToggle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
