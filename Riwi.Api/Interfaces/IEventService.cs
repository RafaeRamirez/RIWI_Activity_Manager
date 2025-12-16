using Riwi.Api.Dtos;

namespace Riwi.Api.Interfaces
{
    public interface IEventService
    {
        Task<EventDto> CreateAsync(CreateEventDto dto);
        Task<EventDto?> GetByIdAsync(long id);
        Task<IEnumerable<EventDto>> GetAllAsync();
        Task<EventDto> UpdateAsync(long id, UpdateEventDto dto);
        Task DeleteAsync(long id);
        
        // Event Sessions
        Task<EventSessionDto> AddSessionAsync(CreateEventSessionDto dto);
        Task<IEnumerable<EventSessionDto>> GetSessionsByEventIdAsync(long eventId);
        
        // Event Speakers
        Task AddSpeakerAsync(CreateEventSpeakerDto dto);
        Task RemoveSpeakerAsync(long eventId, long speakerId);
    }
}
