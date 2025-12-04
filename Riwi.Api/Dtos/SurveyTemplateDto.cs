using System.ComponentModel.DataAnnotations;
using Riwi.Api.Enums;

namespace Riwi.Api.Dtos
{
    public class CreateSurveyTemplateDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public SurveyPurpose Purpose { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateSurveyTemplateDto
    {
        public string? Name { get; set; }

        public SurveyPurpose? Purpose { get; set; }

        public bool? IsActive { get; set; }
    }

    public class SurveyTemplateDto
    {
        public long TemplateId { get; set; }

        public string Name { get; set; } = null!;

        public SurveyPurpose Purpose { get; set; }

        public bool IsActive { get; set; }
    }
}
