using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateCohortDto
    {
        [Required]
        public long ProgramId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class UpdateCohortDto
    {
        public long? ProgramId { get; set; }

        public string? Name { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class CohortDto
    {
        public long CohortId { get; set; }

        public long ProgramId { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
