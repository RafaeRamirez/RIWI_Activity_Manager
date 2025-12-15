using System;
using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Models;

public class Event
{
    public long EventId { get; set; }           // BIGSERIAL → long
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string EventType { get; set; } = null!;
    
    // Relación con locations
    public long? LocationId { get; set; }
    public Location? Location { get; set; }

    public int Capacity { get; set; }           // CHECK (>=0) válido con data annotations o Fluent API
    public int? WaitlistLimit { get; set; }

    public bool RequiresCheckin { get; set; } = true;
    public bool IsPublished { get; set; } = false;

    public DateTimeOffset? PublishedAt { get; set; }

    // Relación con People
    public long? CreatedBy { get; set; }
    public Person? Creator { get; set; }

    public DateTimeOffset StartAt { get; set; }
    public DateTimeOffset EndAt { get; set; }

    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<EventSpeaker> EventSpeakers { get; set; } = new List<EventSpeaker>();
    public ICollection<EventTag> EventTags { get; set; } = new List<EventTag>();
    public ICollection<EventSession> Sessions { get; set; } = new List<EventSession>();
    public ICollection<SurveyResponse> SurveyResponses { get; set; } = new List<SurveyResponse>();
}
