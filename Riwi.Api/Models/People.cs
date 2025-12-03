using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Riwi.Api.Models
{
    [Table("people")]
    public class People
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

        [Required]
        [Column("role")]
        public string Role { get; set; } // coder | admin

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}