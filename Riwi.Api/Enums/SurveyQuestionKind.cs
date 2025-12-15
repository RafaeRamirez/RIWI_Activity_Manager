using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Enums;

public enum SurveyQuestionKind
{
    Likert,     // Escala 1–5
    Nps,        // Net Promoter Score
    Open,       // Pregunta abierta de texto
    Boolean,    // Sí / No
    Numeric     // Número libre
}
