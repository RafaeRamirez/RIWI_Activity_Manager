using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateEventRequirementDto
    {
        [Required]
        public long EventId { get; set; }

        [Required]
        public string Kind { get; set; } = null!;

        [Required]
        public string Value { get; set; } = null!;
    }

    public class UpdateEventRequirementDto
    {
        public string? Kind { get; set; }

        public string? Value { get; set; }
    }

    public class EventRequirementDto
    {
        public long RequirementId { get; set; }

        public long EventId { get; set; }

        public string Kind { get; set; } = null!;

        public string Value { get; set; } = null!;
    }
}
