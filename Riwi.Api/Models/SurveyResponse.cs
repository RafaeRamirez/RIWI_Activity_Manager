using Riwi.Api.Models;

namespace Riwi.Api.Models;

public class SurveyResponse
{
    public long ResponseId { get; set; }                        // BIGSERIAL

    public long EventId { get; set; }                           // FK → events
    public Event? Event { get; set; }

    public long PersonId { get; set; }                          // FK → people
    public Person? Person { get; set; }

    public long TemplateId { get; set; }                        // FK → survey_templates
    public SurveyTemplate? Template { get; set; }

    public DateTimeOffset SubmittedAt { get; set; } = DateTimeOffset.UtcNow;
}
