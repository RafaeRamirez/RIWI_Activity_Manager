using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateSurveyAnswerDto
    {
        [Required]
        public long ResponseId { get; set; }

        [Required]
        public long QuestionId { get; set; }

        public string? AnswerText { get; set; }
    }

    public class SurveyAnswerDto
    {
        public long ResponseId { get; set; }

        public long QuestionId { get; set; }

        public string? AnswerText { get; set; }
    }
}
