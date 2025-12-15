using Riwi.Api.Dtos;

namespace Riwi.Api.Interfaces
{
    public interface IOrganizationService
    {
        Task<OrganizationDto> CreateAsync(CreateOrganizationDto dto);
        Task<OrganizationDto?> GetByIdAsync(long id);
        Task<IEnumerable<OrganizationDto>> GetAllAsync();
        Task<OrganizationDto> UpdateAsync(long id, UpdateOrganizationDto dto);
        Task DeleteAsync(long id);
    }
}
