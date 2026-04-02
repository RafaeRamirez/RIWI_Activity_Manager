using System.ComponentModel.DataAnnotations;
using Riwi.Api.Enums;

namespace Riwi.Api.Dtos
{
    public class CreateNotificationLogDto
    {
        public long? EventId { get; set; }

        public long? PersonId { get; set; }

        [Required]
        public NotificationChannel Channel { get; set; }

        public string? Template { get; set; }

        public string? Status { get; set; }
    }

    public class NotificationLogDto
    {
        public long NotificationId { get; set; }

        public long? EventId { get; set; }

        public long? PersonId { get; set; }

        public NotificationChannel Channel { get; set; }

        public string? Template { get; set; }

        public DateTimeOffset SentAt { get; set; }

        public string? Status { get; set; }
    }
}
