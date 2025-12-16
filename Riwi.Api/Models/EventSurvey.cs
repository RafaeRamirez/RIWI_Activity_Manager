using Riwi.Api.Models;

namespace Riwi.Api.Models;

public class EventSurvey
{
    // 🔑 Composite Key → event_id + template_id
    public long EventId { get; set; }
    public Event? Event { get; set; }

    public long TemplateId { get; set; }
    public SurveyTemplate? Template { get; set; }

    public DateTimeOffset? OpenAt { get; set; }
    public DateTimeOffset? CloseAt { get; set; }
}
