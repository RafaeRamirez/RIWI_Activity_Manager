using Riwi.Api.Dtos;

namespace Riwi.Api.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> CreateAsync(CreatePersonDto dto);
        Task<PersonDto?> GetByIdAsync(long id);
        Task<PersonDto?> GetByEmailAsync(string email);
        Task<IEnumerable<PersonDto>> GetAllAsync();
        Task<PersonDto> UpdateAsync(long id, UpdatePersonDto dto);
        Task DeleteAsync(long id);
        
        // Speakers
        Task<SpeakerDto> CreateSpeakerProfileAsync(CreateSpeakerDto dto);
        Task<SpeakerDto?> GetSpeakerProfileAsync(long personId);
    }
}
