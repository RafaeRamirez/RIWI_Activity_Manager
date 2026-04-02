using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class EventTag
{
    public long EventId { get; set; }
    public Event Event { get; set; } = null!;

    public long TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}
