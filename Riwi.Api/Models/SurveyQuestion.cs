using Riwi.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class SurveyQuestion
{
    [Key]
    public long QuestionId { get; set; }                      // BIGSERIAL

    public long TemplateId { get; set; }                      // FK → survey_templates
    public SurveyTemplate? Template { get; set; }             // Navegación

    public string QuestionText { get; set; } = null!;         // NOT NULL

    public SurveyQuestionKind Kind { get; set; }              // ENUM CHECK

    public int Position { get; set; }                         // Orden de pregunta en la encuesta
}
