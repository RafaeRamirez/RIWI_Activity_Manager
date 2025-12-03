using Riwi.Api.Enums;

namespace Riwi.Api.Models;

public class NotificationLog
{
    public long NotificationId { get; set; }                     // BIGSERIAL PK

    public long? EventId { get; set; }                           // Opcional (no NOT NULL)
    public Event? Event { get; set; }

    public long? PersonId { get; set; }                          // Opcional (no NOT NULL)
    public Person? Person { get; set; }

    public NotificationChannel Channel { get; set; }             // 'email' | 'discord'

    public string? Template { get; set; }                        // plantilla usada (html, texto, etc.)

    public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow; // DEFAULT now()

    public string? Status { get; set; }                          // delivered, failed, queued, etc.
}
