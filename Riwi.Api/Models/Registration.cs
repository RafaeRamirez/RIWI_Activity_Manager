using Riwi.Api.Enums;

namespace Riwi.Api.Models;

public class Registration
{
    public long RegistrationId { get; set; }

    public long EventId { get; set; }
    public Event Event { get; set; } = null!;

    public long PersonId { get; set; }
    public Person Person { get; set; } = null!;

    public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;

    public DateTimeOffset? AppliedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ConfirmedAt { get; set; }
    public DateTimeOffset? CancelledAt { get; set; }

    public int? WaitlistPosition { get; set; }
}
