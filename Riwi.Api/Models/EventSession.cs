using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class EventSession
{
    [Key]
    public long SessionId { get; set; }              // BIGSERIAL → long

    public long EventId { get; set; }                // FK obligatorio
    public Event Event { get; set; } = null!;        // Relación requerida

    public string? Title { get; set; }

    public DateTimeOffset StartsAt { get; set; }     // TIMESTAMPTZ → DateTimeOffset
    public DateTimeOffset EndsAt { get; set; }

    public List<Attendance> Attendances { get; set; } = new();
}
