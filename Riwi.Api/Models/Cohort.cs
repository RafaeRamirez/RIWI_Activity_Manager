using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class Cohort
{
    public long CohortId { get; set; }

    [Required]
    public long ProgramId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Navigation property
    public Route? Route { get; set; }

}