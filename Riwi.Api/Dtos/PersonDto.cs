using System.ComponentModel.DataAnnotations;
using Riwi.Api.Enums;

namespace Riwi.Api.Dtos
{
    public class CreatePersonDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        public string? DiscordHandle { get; set; }

        public string? Phone { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }

    public class UpdatePersonDto
    {
        [EmailAddress]
        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? DiscordHandle { get; set; }

        public string? Phone { get; set; }

        public UserRole? Role { get; set; }
    }

    public class PersonDto
    {
        public long PersonId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string? DiscordHandle { get; set; }

        public string? Phone { get; set; }

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public CoderProfileDto? CoderProfile { get; set; }
    }
}
