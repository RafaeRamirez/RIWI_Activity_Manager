using System.ComponentModel.DataAnnotations;

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
        public string Role { get; set; } = string.Empty;
    }

    public class UpdatePersonDto
    {
        [EmailAddress]
        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? DiscordHandle { get; set; }

        public string? Phone { get; set; }

        public string? Role { get; set; }
    }

    public class PersonDto
    {
        public long PersonId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string? DiscordHandle { get; set; }

        public string? Phone { get; set; }

        public string Role { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
