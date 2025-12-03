using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateCoderProfileDto
    {
        [Required]
        public long PersonId { get; set; }

        public long? CohortId { get; set; }

        public string? Level { get; set; }

        public string? Status { get; set; }
    }

    public class UpdateCoderProfileDto
    {
        public long? CohortId { get; set; }

        public string? Level { get; set; }

        public string? Status { get; set; }
    }

    public class CoderProfileDto
    {
        public long PersonId { get; set; }

        public long? CohortId { get; set; }

        public string? Level { get; set; }

        public string? Status { get; set; }
    }
}
