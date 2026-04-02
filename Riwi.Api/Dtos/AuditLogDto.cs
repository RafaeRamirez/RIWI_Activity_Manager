using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Riwi.Api.Dtos
{
    public class CreateAuditLogDto
    {
        public long? ActorId { get; set; }

        [Required]
        public string Action { get; set; } = null!;

        [Required]
        public string Entity { get; set; } = null!;

        [Required]
        public long EntityId { get; set; }

        public JsonDocument? Details { get; set; }
    }

    public class AuditLogDto
    {
        public long AuditId { get; set; }

        public long? ActorId { get; set; }

        public string Action { get; set; } = null!;

        public string Entity { get; set; } = null!;

        public long EntityId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public JsonDocument? Details { get; set; }
    }
}
