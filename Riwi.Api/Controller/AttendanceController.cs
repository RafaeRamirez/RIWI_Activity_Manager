using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _repository;
        private readonly IMapper _mapper;

        public AttendanceController(IAttendanceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAll()
        {
            var attendances = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AttendanceDto>>(attendances));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDto>> GetById(long id)
        {
            var attendance = await _repository.GetByIdAsync(id);
            if (attendance == null) return NotFound();
            return Ok(_mapper.Map<AttendanceDto>(attendance));
        }

        [HttpPost]
        public async Task<ActionResult<AttendanceDto>> Create(CreateAttendanceDto dto)
        {
            var attendance = _mapper.Map<Attendance>(dto);
            await _repository.AddAsync(attendance);
            return CreatedAtAction(nameof(GetById), new { id = attendance.AttendanceId }, _mapper.Map<AttendanceDto>(attendance));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var attendance = await _repository.GetByIdAsync(id);
            if (attendance == null) return NotFound();

            await _repository.DeleteAsync(attendance);
            return NoContent();
        }
    }
}
