using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class Tag
{
    [Key]
    public long TagId { get; set; }         // BIGSERIAL → long
    public string Name { get; set; } = null!;

    public ICollection<EventTag> EventTags { get; set; } = new List<EventTag>();
}
