using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateEventSurveyDto
    {
        [Required]
        public long EventId { get; set; }

        [Required]
        public long TemplateId { get; set; }

        public DateTimeOffset? OpenAt { get; set; }

        public DateTimeOffset? CloseAt { get; set; }
    }

    public class UpdateEventSurveyDto
    {
        public DateTimeOffset? OpenAt { get; set; }

        public DateTimeOffset? CloseAt { get; set; }
    }

    public class EventSurveyDto
    {
        public long EventId { get; set; }

        public long TemplateId { get; set; }

        public DateTimeOffset? OpenAt { get; set; }

        public DateTimeOffset? CloseAt { get; set; }
    }
}
