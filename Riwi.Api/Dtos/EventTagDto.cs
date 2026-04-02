using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateEventTagDto
    {
        [Required]
        public long EventId { get; set; }

        [Required]
        public long TagId { get; set; }
    }

    public class EventTagDto
    {
        public long EventId { get; set; }

        public long TagId { get; set; }
    }
}
