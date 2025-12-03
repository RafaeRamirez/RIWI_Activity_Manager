using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class Speaker
{
    public long SpeakerId { get; set; }          // BIGSERIAL → long

    public long? PersonId { get; set; }          // Relación opcional
    public Person? Person { get; set; }

    public long? OrgId { get; set; }             // Relación opcional
    public Organization? Organization { get; set; }

    public string? Bio { get; set; }

    public ICollection<EventSpeaker> EventSpeakers { get; set; } = new List<EventSpeaker>();
}
