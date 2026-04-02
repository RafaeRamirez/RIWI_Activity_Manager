using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class Attendance
{
    [Key]
    public long AttendanceId { get; set; }

    public long EventId { get; set; }
    public Event Event { get; set; } = null!;

    public long? SessionId { get; set; }
    public EventSession? Session { get; set; }

    public long PersonId { get; set; }
    public Person Person { get; set; } = null!;     

    public DateTime CheckinTime { get; set; } = DateTime.UtcNow;
    public string Method { get; set; } // qr | manual | discord
}
