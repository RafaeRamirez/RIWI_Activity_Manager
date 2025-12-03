using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class CoderProfile
{
    [Key]
    public long PersonId { get; set; }

    public long? CohortId { get; set; }

    public string? Level { get; set; }

    public string? Status { get; set; }

    // Navigation properties
    public Person? Person { get; set; }
    public Cohort? Cohort { get; set; }
}