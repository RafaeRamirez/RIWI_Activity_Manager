using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    /// <summary>
    /// Controller for managing event registrations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(IRegistrationRepository repository, IMapper mapper, ILogger<RegistrationController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all registrations
        /// </summary>
        /// <returns>List of all registrations</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RegistrationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RegistrationDto>>> GetAll()
        {
            try
            {
                var registrations = await _repository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<RegistrationDto>>(registrations));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all registrations");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving registrations",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Get registration by ID
        /// </summary>
        /// <param name="id">Registration ID</param>
        /// <returns>Registration details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegistrationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegistrationDto>> GetById(long id)
        {
            try
            {
                var registration = await _repository.GetByIdAsync(id);
                if (registration == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Registration with ID {id} not found"
                    });
                }
                return Ok(_mapper.Map<RegistrationDto>(registration));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving registration {RegistrationId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving the registration",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a new registration
        /// </summary>
        /// <param name="dto">Registration creation data</param>
        /// <returns>Created registration</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RegistrationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegistrationDto>> Create(CreateRegistrationDto dto)
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

                var registration = _mapper.Map<Registration>(dto);
                await _repository.AddAsync(registration);
                return CreatedAtAction(nameof(GetById), new { id = registration.RegistrationId }, _mapper.Map<RegistrationDto>(registration));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating registration");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while creating the registration",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Update an existing registration
        /// </summary>
        /// <param name="id">Registration ID</param>
        /// <param name="dto">Registration update data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(long id, UpdateRegistrationDto dto)
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

                var registration = await _repository.GetByIdAsync(id);
                if (registration == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Registration with ID {id} not found"
                    });
                }

                _mapper.Map(dto, registration);
                await _repository.UpdateAsync(registration);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating registration {RegistrationId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating the registration",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete a registration
        /// </summary>
        /// <param name="id">Registration ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var registration = await _repository.GetByIdAsync(id);
                if (registration == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Registration with ID {id} not found"
                    });
                }

                await _repository.DeleteAsync(registration);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting registration {RegistrationId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while deleting the registration",
                    Details = ex.Message
                });
            }
        }
    }
}
