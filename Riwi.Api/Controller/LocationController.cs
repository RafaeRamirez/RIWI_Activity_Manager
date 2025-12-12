using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    /// <summary>
    /// Controller for managing locations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILocationRepository repository, IMapper mapper, ILogger<LocationController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all locations
        /// </summary>
        /// <returns>List of all locations</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
        {
            try
            {
                var locations = await _repository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<LocationDto>>(locations));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all locations");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving locations",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Get location by ID
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <returns>Location details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationDto>> GetById(long id)
        {
            try
            {
                var location = await _repository.GetByIdAsync(id);
                if (location == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Location with ID {id} not found"
                    });
                }
                return Ok(_mapper.Map<LocationDto>(location));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving location {LocationId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving the location",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a new location
        /// </summary>
        /// <param name="dto">Location creation data</param>
        /// <returns>Created location</returns>
        [HttpPost]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationDto>> Create(CreateLocationDto dto)
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

                var location = _mapper.Map<Location>(dto);
                await _repository.AddAsync(location);
                return CreatedAtAction(nameof(GetById), new { id = location.LocationId }, _mapper.Map<LocationDto>(location));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating location");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while creating the location",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Update an existing location
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <param name="dto">Location update data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(long id, UpdateLocationDto dto)
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

                var location = await _repository.GetByIdAsync(id);
                if (location == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Location with ID {id} not found"
                    });
                }

                _mapper.Map(dto, location);
                await _repository.UpdateAsync(location);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating location {LocationId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating the location",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete a location
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var location = await _repository.GetByIdAsync(id);
                if (location == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Location with ID {id} not found"
                    });
                }

                await _repository.DeleteAsync(location);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting location {LocationId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while deleting the location",
                    Details = ex.Message
                });
            }
        }
    }
}
