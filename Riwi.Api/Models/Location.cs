using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class Location
{
    public long LocationId { get; set; }

    [Required]
    public string Kind { get; set; } = string.Empty;

    public string? Address { get; set; }

    public string? Room { get; set; }

    public string? Url { get; set; }

    public ICollection<Event> Events { get; set; } = new List<Event>();
}