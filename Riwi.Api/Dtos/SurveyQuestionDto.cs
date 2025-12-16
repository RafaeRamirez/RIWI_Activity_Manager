using System.ComponentModel.DataAnnotations;
using Riwi.Api.Enums;

namespace Riwi.Api.Dtos
{
    public class CreateSurveyQuestionDto
    {
        [Required]
        public long TemplateId { get; set; }

        [Required]
        public string QuestionText { get; set; } = null!;

        [Required]
        public SurveyQuestionKind Kind { get; set; }

        public int Position { get; set; }
    }

    public class UpdateSurveyQuestionDto
    {
        public string? QuestionText { get; set; }

        public SurveyQuestionKind? Kind { get; set; }

        public int? Position { get; set; }
    }

    public class SurveyQuestionDto
    {
        public long QuestionId { get; set; }

        public long TemplateId { get; set; }

        public string QuestionText { get; set; } = null!;

        public SurveyQuestionKind Kind { get; set; }

        public int Position { get; set; }
    }
}
