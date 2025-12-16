using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateCheckinTokenDto
    {
        [Required]
        public long EventId { get; set; }

        [Required]
        public long PersonId { get; set; }

        public long? SessionId { get; set; }

        [Required]
        public string TokenHash { get; set; } = null!;

        [Required]
        public DateTimeOffset ExpiresAt { get; set; }
    }

    public class CheckinTokenDto
    {
        public long TokenId { get; set; }

        public long EventId { get; set; }

        public long PersonId { get; set; }

        public long? SessionId { get; set; }

        public string TokenHash { get; set; } = null!;

        public DateTimeOffset ExpiresAt { get; set; }

        public DateTimeOffset? UsedAt { get; set; }
    }
}
