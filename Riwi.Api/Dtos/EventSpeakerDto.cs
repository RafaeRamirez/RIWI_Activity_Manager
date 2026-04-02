using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateEventSpeakerDto
    {
        [Required]
        public long EventId { get; set; }

        [Required]
        public long SpeakerId { get; set; }

        public string? Role { get; set; }
    }

    public class EventSpeakerDto
    {
        public long EventId { get; set; }

        public long SpeakerId { get; set; }

        public string? Role { get; set; }
    }
}
