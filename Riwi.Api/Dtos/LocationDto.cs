using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateLocationDto
    {
        [Required]
        public string Kind { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? Room { get; set; }

        public string? Url { get; set; }
    }

    public class UpdateLocationDto
    {
        public string? Kind { get; set; }

        public string? Address { get; set; }

        public string? Room { get; set; }

        public string? Url { get; set; }
    }

    public class LocationDto
    {
        public long LocationId { get; set; }

        public string Kind { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? Room { get; set; }

        public string? Url { get; set; }
    }
}
