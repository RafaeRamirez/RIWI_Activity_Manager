using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    /// <summary>
    /// Controller for managing event attendance
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AttendanceController> _logger;

        public AttendanceController(IAttendanceRepository repository, IMapper mapper, ILogger<AttendanceController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all attendance records
        /// </summary>
        /// <returns>List of all attendance records</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AttendanceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAll()
        {
            try
            {
                var attendances = await _repository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<AttendanceDto>>(attendances));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all attendances");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving attendance records",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Get attendance record by ID
        /// </summary>
        /// <param name="id">Attendance ID</param>
        /// <returns>Attendance details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AttendanceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AttendanceDto>> GetById(long id)
        {
            try
            {
                var attendance = await _repository.GetByIdAsync(id);
                if (attendance == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Attendance record with ID {id} not found"
                    });
                }
                return Ok(_mapper.Map<AttendanceDto>(attendance));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving attendance {AttendanceId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving the attendance record",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a new attendance record
        /// </summary>
        /// <param name="dto">Attendance creation data</param>
        /// <returns>Created attendance record</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AttendanceDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AttendanceDto>> Create(CreateAttendanceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse
                    {
                        StatusCode = 400,
                        Message = "Invalid request data",
                        Details = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                    });
                }

                var attendance = _mapper.Map<Attendance>(dto);
                await _repository.AddAsync(attendance);
                return CreatedAtAction(nameof(GetById), new { id = attendance.AttendanceId }, _mapper.Map<AttendanceDto>(attendance));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating attendance");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while creating the attendance record",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete an attendance record
        /// </summary>
        /// <param name="id">Attendance ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var attendance = await _repository.GetByIdAsync(id);
                if (attendance == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Attendance record with ID {id} not found"
                    });
                }

                await _repository.DeleteAsync(attendance);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting attendance {AttendanceId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while deleting the attendance record",
                    Details = ex.Message
                });
            }
        }
    }
}
