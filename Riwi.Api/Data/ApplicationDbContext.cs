using Microsoft.EntityFrameworkCore;
using Riwi.Api.Models;

namespace Riwi.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Riwi.Api.Models.Route> Routes { get; set; }
    public DbSet<Cohort> Cohorts { get; set; }
    public DbSet<CoderProfile> CoderProfiles { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<EventSpeaker> EventSpeakers { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<EventTag> EventTags { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<EventSession> EventSessions { get; set; }
    public DbSet<SurveyTemplate> SurveyTemplates { get; set; }
    public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
    public DbSet<SurveyResponse> SurveyResponses { get; set; }
    public DbSet<EventSurvey> EventSurveys { get; set; }
    public DbSet<EventRequirement> EventRequirements { get; set; }
    public DbSet<CheckinToken> CheckinTokens { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<NotificationLog> NotificationLogs { get; set; }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>()
            .HasIndex(p => p.Email)
            .IsUnique();

        modelBuilder.Entity<Person>()
            .Property(p => p.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Person>()
            .HasOne(p => p.CoderProfile)
            .WithOne(c => c.Person)
            .HasForeignKey<CoderProfile>(c => c.PersonId);

        modelBuilder.Entity<EventTag>()
            .HasKey(et => new { et.EventId, et.TagId });

        modelBuilder.Entity<EventSpeaker>()
            .HasKey(es => new { es.EventId, es.SpeakerId });

        modelBuilder.Entity<SurveyAnswer>()
            .HasKey(sa => new { sa.ResponseId, sa.QuestionId });

        modelBuilder.Entity<EventSurvey>()
            .HasKey(es => new { es.EventId, es.TemplateId });

        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Session)
            .WithMany(s => s.Attendances)
            .HasForeignKey(a => a.SessionId)
            .IsRequired(false);
        }
}