using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Riwi.Api.Enums;

namespace Riwi.Api.Models
{
    [Table("people")]
    public class Person
    {
        [Key]
        [Column("person_id")]
        public long PersonId { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("full_name")]
        public string FullName { get; set; }

        [Column("discord_handle")]
        public string? DiscordHandle { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("password")]
        [JsonIgnore]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column("role")]
        public UserRole Role { get; set; } // coder | admin

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();
        public ICollection<SurveyResponse> SurveyResponses { get; set; } = new List<SurveyResponse>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
        public Speaker? SpeakerProfile { get; set; }
        public CoderProfile? CoderProfile { get; set; }
    }
}