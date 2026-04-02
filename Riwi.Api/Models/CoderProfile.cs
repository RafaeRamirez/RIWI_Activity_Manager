using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Riwi.Api.Models;

public class CoderProfile
{
    [Key]
    [ForeignKey("Person")]
    public long PersonId { get; set; }

    public long? CohortId { get; set; }

    public string? Level { get; set; }

    public string? Status { get; set; }

    // Navigation properties
    public Person Person { get; set; } = null!;
    public Cohort? Cohort { get; set; }
}