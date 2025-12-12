using System.ComponentModel.DataAnnotations;
using Riwi.Api.Enums;

namespace Riwi.Api.Models;

public class Organization
{
    [Key]
    public long OrgId { get; set; }            // BIGSERIAL → long
    public string Name { get; set; } = null!;  // NOT NULL

    public string? Kind { get; set; }
    public string? Website { get; set; }

    public ICollection<Speaker> Speakers { get; set; } = new List<Speaker>();
}
