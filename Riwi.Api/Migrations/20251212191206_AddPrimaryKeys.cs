using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Riwi.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoderProfiles_people_PersonId1",
                table: "CoderProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Cohorts_Programs_ProgramId",
                table: "Cohorts");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Cohorts_ProgramId",
                table: "Cohorts");

            migrationBuilder.DropIndex(
                name: "IX_CoderProfiles_PersonId1",
                table: "CoderProfiles");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "CoderProfiles");

            migrationBuilder.AddColumn<long>(
                name: "RouteProgramId",
                table: "Cohorts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PersonId",
                table: "CoderProfiles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActorId = table.Column<long>(type: "bigint", nullable: true),
                    Action = table.Column<string>(type: "text", nullable: false),
                    Entity = table.Column<string>(type: "text", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Details = table.Column<JsonDocument>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditId);
                    table.ForeignKey(
                        name: "FK_AuditLogs_people_ActorId",
                        column: x => x.ActorId,
                        principalTable: "people",
                        principalColumn: "person_id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EventType = table.Column<string>(type: "text", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    WaitlistLimit = table.Column<int>(type: "integer", nullable: true),
                    RequiresCheckin = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    PublishedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatorPersonId = table.Column<long>(type: "bigint", nullable: true),
                    StartAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId");
                    table.ForeignKey(
                        name: "FK_Events_people_CreatorPersonId",
                        column: x => x.CreatorPersonId,
                        principalTable: "people",
                        principalColumn: "person_id");
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrgId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Kind = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrgId);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    ProgramId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.ProgramId);
                });

            migrationBuilder.CreateTable(
                name: "SurveyTemplates",
                columns: table => new
                {
                    TemplateId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Purpose = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyTemplates", x => x.TemplateId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "EventRequirements",
                columns: table => new
                {
                    RequirementId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Kind = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRequirements", x => x.RequirementId);
                    table.ForeignKey(
                        name: "FK_EventRequirements_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventSessions",
                columns: table => new
                {
                    SessionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    StartsAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndsAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_EventSessions_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationLogs",
                columns: table => new
                {
                    NotificationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<long>(type: "bigint", nullable: true),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    Channel = table.Column<int>(type: "integer", nullable: false),
                    Template = table.Column<string>(type: "text", nullable: true),
                    SentAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLogs", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_NotificationLogs_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                    table.ForeignKey(
                        name: "FK_NotificationLogs_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "people",
                        principalColumn: "person_id");
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AppliedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ConfirmedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CancelledAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    WaitlistPosition = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registrations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    SpeakerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    OrgId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationOrgId = table.Column<long>(type: "bigint", nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.SpeakerId);
                    table.ForeignKey(
                        name: "FK_Speakers_Organizations_OrganizationOrgId",
                        column: x => x.OrganizationOrgId,
                        principalTable: "Organizations",
                        principalColumn: "OrgId");
                    table.ForeignKey(
                        name: "FK_Speakers_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "people",
                        principalColumn: "person_id");
                });

            migrationBuilder.CreateTable(
                name: "EventSurveys",
                columns: table => new
                {
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    TemplateId = table.Column<long>(type: "bigint", nullable: false),
                    OpenAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CloseAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSurveys", x => new { x.EventId, x.TemplateId });
                    table.ForeignKey(
                        name: "FK_EventSurveys_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSurveys_SurveyTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "SurveyTemplates",
                        principalColumn: "TemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    QuestionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TemplateId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_SurveyTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "SurveyTemplates",
                        principalColumn: "TemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyResponses",
                columns: table => new
                {
                    ResponseId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    TemplateId = table.Column<long>(type: "bigint", nullable: false),
                    SubmittedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponses", x => x.ResponseId);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_SurveyTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "SurveyTemplates",
                        principalColumn: "TemplateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTags",
                columns: table => new
                {
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    TagId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTags", x => new { x.EventId, x.TagId });
                    table.ForeignKey(
                        name: "FK_EventTags_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    SessionId = table.Column<long>(type: "bigint", nullable: true),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    CheckinTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Method = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendances_EventSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "EventSessions",
                        principalColumn: "SessionId");
                    table.ForeignKey(
                        name: "FK_Attendances_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckinTokens",
                columns: table => new
                {
                    TokenId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    SessionId = table.Column<long>(type: "bigint", nullable: true),
                    TokenHash = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UsedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckinTokens", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_CheckinTokens_EventSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "EventSessions",
                        principalColumn: "SessionId");
                    table.ForeignKey(
                        name: "FK_CheckinTokens_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckinTokens_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventSpeakers",
                columns: table => new
                {
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    SpeakerId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpeakers", x => new { x.EventId, x.SpeakerId });
                    table.ForeignKey(
                        name: "FK_EventSpeakers_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSpeakers_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "SpeakerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyAnswers",
                columns: table => new
                {
                    ResponseId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswers", x => new { x.ResponseId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_SurveyQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_SurveyResponses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "SurveyResponses",
                        principalColumn: "ResponseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_RouteProgramId",
                table: "Cohorts",
                column: "RouteProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EventId",
                table: "Attendances",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_PersonId",
                table: "Attendances",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_SessionId",
                table: "Attendances",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ActorId",
                table: "AuditLogs",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckinTokens_EventId",
                table: "CheckinTokens",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckinTokens_PersonId",
                table: "CheckinTokens",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckinTokens_SessionId",
                table: "CheckinTokens",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRequirements_EventId",
                table: "EventRequirements",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatorPersonId",
                table: "Events",
                column: "CreatorPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSessions_EventId",
                table: "EventSessions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpeakers_SpeakerId",
                table: "EventSpeakers",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSurveys_TemplateId",
                table: "EventSurveys",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTags_TagId",
                table: "EventTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_EventId",
                table: "NotificationLogs",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_PersonId",
                table: "NotificationLogs",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EventId",
                table: "Registrations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_PersonId",
                table: "Registrations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_OrganizationOrgId",
                table: "Speakers",
                column: "OrganizationOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_PersonId",
                table: "Speakers",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_QuestionId",
                table: "SurveyAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_TemplateId",
                table: "SurveyQuestions",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_EventId",
                table: "SurveyResponses",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_PersonId",
                table: "SurveyResponses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_TemplateId",
                table: "SurveyResponses",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoderProfiles_people_PersonId",
                table: "CoderProfiles",
                column: "PersonId",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cohorts_Routes_RouteProgramId",
                table: "Cohorts",
                column: "RouteProgramId",
                principalTable: "Routes",
                principalColumn: "ProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoderProfiles_people_PersonId",
                table: "CoderProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Cohorts_Routes_RouteProgramId",
                table: "Cohorts");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "CheckinTokens");

            migrationBuilder.DropTable(
                name: "EventRequirements");

            migrationBuilder.DropTable(
                name: "EventSpeakers");

            migrationBuilder.DropTable(
                name: "EventSurveys");

            migrationBuilder.DropTable(
                name: "EventTags");

            migrationBuilder.DropTable(
                name: "NotificationLogs");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "SurveyAnswers");

            migrationBuilder.DropTable(
                name: "EventSessions");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.DropTable(
                name: "SurveyResponses");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "SurveyTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Cohorts_RouteProgramId",
                table: "Cohorts");

            migrationBuilder.DropColumn(
                name: "RouteProgramId",
                table: "Cohorts");

            migrationBuilder.AlterColumn<long>(
                name: "PersonId",
                table: "CoderProfiles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "PersonId1",
                table: "CoderProfiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    ProgramId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_ProgramId",
                table: "Cohorts",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_CoderProfiles_PersonId1",
                table: "CoderProfiles",
                column: "PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CoderProfiles_people_PersonId1",
                table: "CoderProfiles",
                column: "PersonId1",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cohorts_Programs_ProgramId",
                table: "Cohorts",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "ProgramId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
