using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateEventSessionDto
    {
        [Required]
        public long EventId { get; set; }

        public string? Title { get; set; }

        [Required]
        public DateTimeOffset StartsAt { get; set; }

        [Required]
        public DateTimeOffset EndsAt { get; set; }
    }

    public class UpdateEventSessionDto
    {
        public string? Title { get; set; }

        public DateTimeOffset? StartsAt { get; set; }

        public DateTimeOffset? EndsAt { get; set; }
    }

    public class EventSessionDto
    {
        public long SessionId { get; set; }

        public long EventId { get; set; }

        public string? Title { get; set; }

        public DateTimeOffset StartsAt { get; set; }

        public DateTimeOffset EndsAt { get; set; }
    }
}
