using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    /// <summary>
    /// Controller for managing events
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<EventController> _logger;

        public EventController(IEventRepository repository, IMapper mapper, ILogger<EventController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all events
        /// </summary>
        /// <returns>List of all events</returns>
        /// <response code="200">Returns the list of events</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EventDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAll()
        {
            try
            {
                var events = await _repository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<EventDto>>(events));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all events");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving events",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Get event by ID
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns>Event details</returns>
        /// <response code="200">Returns the event</response>
        /// <response code="404">Event not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EventDto>> GetById(long id)
        {
            try
            {
                var eventEntity = await _repository.GetByIdAsync(id);
                if (eventEntity == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Event with ID {id} not found"
                    });
                }
                return Ok(_mapper.Map<EventDto>(eventEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving event {EventId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving the event",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a new event
        /// </summary>
        /// <param name="dto">Event creation data</param>
        /// <returns>Created event</returns>
        /// <response code="201">Event created successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EventDto>> Create(CreateEventDto dto)
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

                var eventEntity = _mapper.Map<Event>(dto);
                await _repository.AddAsync(eventEntity);
                return CreatedAtAction(nameof(GetById), new { id = eventEntity.EventId }, _mapper.Map<EventDto>(eventEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating event");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while creating the event",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Update an existing event
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <param name="dto">Event update data</param>
        /// <returns>No content</returns>
        /// <response code="204">Event updated successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Event not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(long id, UpdateEventDto dto)
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

                var eventEntity = await _repository.GetByIdAsync(id);
                if (eventEntity == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Event with ID {id} not found"
                    });
                }

                _mapper.Map(dto, eventEntity);
                await _repository.UpdateAsync(eventEntity);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating event {EventId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating the event",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete an event
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns>No content</returns>
        /// <response code="204">Event deleted successfully</response>
        /// <response code="404">Event not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var eventEntity = await _repository.GetByIdAsync(id);
                if (eventEntity == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Event with ID {id} not found"
                    });
                }

                await _repository.DeleteAsync(eventEntity);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting event {EventId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while deleting the event",
                    Details = ex.Message
                });
            }
        }
    }
}
