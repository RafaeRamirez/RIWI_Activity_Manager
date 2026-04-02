using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateAttendanceDto
    {
        [Required]
        public long EventId { get; set; }

        public long? SessionId { get; set; }

        [Required]
        public long PersonId { get; set; }

        public DateTime CheckinTime { get; set; } = DateTime.UtcNow;

        [Required]
        public string Method { get; set; } = null!;
    }

    public class AttendanceDto
    {
        public long AttendanceId { get; set; }

        public long EventId { get; set; }

        public long? SessionId { get; set; }

        public long PersonId { get; set; }

        public DateTime CheckinTime { get; set; }

        public string Method { get; set; } = null!;
    }
}
