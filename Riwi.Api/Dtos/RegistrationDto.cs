using System.ComponentModel.DataAnnotations;
using Riwi.Api.Enums;

namespace Riwi.Api.Dtos
{
    public class CreateRegistrationDto
    {
        [Required]
        public long EventId { get; set; }

        [Required]
        public long PersonId { get; set; }

        public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;
    }

    public class UpdateRegistrationDto
    {
        public RegistrationStatus? Status { get; set; }

        public DateTimeOffset? ConfirmedAt { get; set; }

        public DateTimeOffset? CancelledAt { get; set; }

        public int? WaitlistPosition { get; set; }
    }

    public class RegistrationDto
    {
        public long RegistrationId { get; set; }

        public long EventId { get; set; }

        public long PersonId { get; set; }

        public RegistrationStatus Status { get; set; }

        public DateTimeOffset? AppliedAt { get; set; }

        public DateTimeOffset? ConfirmedAt { get; set; }

        public DateTimeOffset? CancelledAt { get; set; }

        public int? WaitlistPosition { get; set; }
    }
}
