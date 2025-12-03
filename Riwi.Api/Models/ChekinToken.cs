using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class CheckinToken
{
    public long TokenId { get; set; }                     // BIGSERIAL

    public long EventId { get; set; }
    public Event Event { get; set; } = null!;            // NOT NULL

    public long PersonId { get; set; }
    public Person Person { get; set; } = null!;          // NOT NULL

    public long? SessionId { get; set; }
    public EventSession? Session { get; set; }           // Nullable (no siempre ligado a una sesión)

    public string TokenHash { get; set; } = null!;       // Hash del QR o JWT cifrado

    public DateTimeOffset ExpiresAt { get; set; }        // Tiempo máximo para usar el token
    public DateTimeOffset? UsedAt { get; set; }          // Cuando se marcó asistencia
}
