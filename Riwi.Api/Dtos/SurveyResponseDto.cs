using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateSurveyResponseDto
    {
        [Required]
        public long EventId { get; set; }

        [Required]
        public long PersonId { get; set; }

        [Required]
        public long TemplateId { get; set; }

        public DateTimeOffset SubmittedAt { get; set; } = DateTimeOffset.UtcNow;
    }

    public class SurveyResponseDto
    {
        public long ResponseId { get; set; }

        public long EventId { get; set; }

        public long PersonId { get; set; }

        public long TemplateId { get; set; }

        public DateTimeOffset SubmittedAt { get; set; }
    }
}
