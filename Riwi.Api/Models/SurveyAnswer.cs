using Riwi.Api.Models;

namespace Riwi.Api.Models;

public class SurveyAnswer
{
    public long ResponseId { get; set; }                      // FK → survey_responses
    public SurveyResponse? Response { get; set; }

    public long QuestionId { get; set; }                      // FK → survey_questions
    public SurveyQuestion? Question { get; set; }

    public string? AnswerText { get; set; }                   // Respuesta (texto, número, boolean, escala)
}
