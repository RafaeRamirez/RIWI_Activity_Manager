using System.ComponentModel.DataAnnotations;
using Riwi.Api.Enums;

namespace Riwi.Api.Models;

public class EventRequirement
{
    [Key]
    public long RequirementId { get; set; }     // BIGSERIAL → long

    public long EventId { get; set; }           // FK obligatorio
    public Event Event { get; set; } = null!;

    public string Kind { get; set; } = null!;   // NOT NULL
    public string Value { get; set; } = null!;  // NOT NULL
}
