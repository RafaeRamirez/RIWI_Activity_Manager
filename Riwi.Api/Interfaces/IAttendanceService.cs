using Riwi.Api.Dtos;

namespace Riwi.Api.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceDto> CheckInAsync(CreateAttendanceDto dto);
        Task<IEnumerable<AttendanceDto>> GetByEventIdAsync(long eventId);
        Task<IEnumerable<AttendanceDto>> GetByPersonIdAsync(long personId);
        Task<CheckinTokenDto> GenerateCheckinTokenAsync(CreateCheckinTokenDto dto);
        Task<bool> ValidateCheckinTokenAsync(string tokenHash);
    }
}
