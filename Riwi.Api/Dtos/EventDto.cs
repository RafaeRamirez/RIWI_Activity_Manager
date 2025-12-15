using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateEventDto
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public string EventType { get; set; } = null!;

        public long? LocationId { get; set; }

        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }

        public int? WaitlistLimit { get; set; }

        public bool RequiresCheckin { get; set; } = true;

        public bool IsPublished { get; set; } = false;

        public DateTimeOffset? PublishedAt { get; set; }

        public long? CreatedBy { get; set; }

        [Required]
        public DateTimeOffset StartAt { get; set; }

        [Required]
        public DateTimeOffset EndAt { get; set; }
    }

    public class UpdateEventDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? EventType { get; set; }

        public long? LocationId { get; set; }

        public int? Capacity { get; set; }

        public int? WaitlistLimit { get; set; }

        public bool? RequiresCheckin { get; set; }

        public bool? IsPublished { get; set; }

        public DateTimeOffset? PublishedAt { get; set; }

        public DateTimeOffset? StartAt { get; set; }

        public DateTimeOffset? EndAt { get; set; }
    }

    public class EventDto
    {
        public long EventId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string EventType { get; set; } = null!;

        public long? LocationId { get; set; }

        public int Capacity { get; set; }

        public int? WaitlistLimit { get; set; }

        public bool RequiresCheckin { get; set; }

        public bool IsPublished { get; set; }

        public DateTimeOffset? PublishedAt { get; set; }

        public long? CreatedBy { get; set; }

        public DateTimeOffset StartAt { get; set; }

        public DateTimeOffset EndAt { get; set; }
    }
}
