using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Enums;

public enum RegistrationStatus
{
    Pending,
    Confirmed,
    Waitlisted,
    Cancelled,
    Attended,
    NoShow
}
