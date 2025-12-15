using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class EventSpeaker
{
    public long EventId { get; set; }
    public Event Event { get; set; } = null!;

    public long SpeakerId { get; set; }
    public Speaker Speaker { get; set; } = null!;

    public string? Role { get; set; }     // Campo adicional
}
