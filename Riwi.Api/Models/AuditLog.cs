using Riwi.Api.Enums;
using System.Text.Json;

namespace Riwi.Api.Models;

public class AuditLog
{
    public long AuditId { get; set; }                 // BIGSERIAL PK

    public long? ActorId { get; set; }                // FK → people (no NOT NULL)
    public Person? Actor { get; set; }

    public string Action { get; set; } = null!;       // acción realizada (delete, update, create, login…)

    public string Entity { get; set; } = null!;       // entidad afectada (Event, Person, Registration…)

    public long EntityId { get; set; }                // ID del registro afectado

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow; // DEFAULT now()

    public JsonDocument? Details { get; set; }        // JSONB → datos adicionales en JSON
}
