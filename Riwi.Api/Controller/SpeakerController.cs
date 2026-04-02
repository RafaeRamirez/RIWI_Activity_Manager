using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    /// <summary>
    /// Controller for managing speakers
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SpeakerController : ControllerBase
    {
        private readonly ISpeakerRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SpeakerController> _logger;

        public SpeakerController(ISpeakerRepository repository, IMapper mapper, ILogger<SpeakerController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all speakers
        /// </summary>
        /// <returns>List of all speakers</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SpeakerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SpeakerDto>>> GetAll()
        {
            try
            {
                var speakers = await _repository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<SpeakerDto>>(speakers));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all speakers");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving speakers",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Get speaker by ID
        /// </summary>
        /// <param name="id">Speaker ID</param>
        /// <returns>Speaker details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SpeakerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SpeakerDto>> GetById(long id)
        {
            try
            {
                var speaker = await _repository.GetByIdAsync(id);
                if (speaker == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Speaker with ID {id} not found"
                    });
                }
                return Ok(_mapper.Map<SpeakerDto>(speaker));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving speaker {SpeakerId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving the speaker",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a new speaker
        /// </summary>
        /// <param name="dto">Speaker creation data</param>
        /// <returns>Created speaker</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SpeakerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SpeakerDto>> Create(CreateSpeakerDto dto)
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

                var speaker = _mapper.Map<Speaker>(dto);
                await _repository.AddAsync(speaker);
                return CreatedAtAction(nameof(GetById), new { id = speaker.SpeakerId }, _mapper.Map<SpeakerDto>(speaker));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating speaker");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while creating the speaker",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Update an existing speaker
        /// </summary>
        /// <param name="id">Speaker ID</param>
        /// <param name="dto">Speaker update data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(long id, UpdateSpeakerDto dto)
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

                var speaker = await _repository.GetByIdAsync(id);
                if (speaker == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Speaker with ID {id} not found"
                    });
                }

                _mapper.Map(dto, speaker);
                await _repository.UpdateAsync(speaker);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating speaker {SpeakerId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating the speaker",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete a speaker
        /// </summary>
        /// <param name="id">Speaker ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var speaker = await _repository.GetByIdAsync(id);
                if (speaker == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Speaker with ID {id} not found"
                    });
                }

                await _repository.DeleteAsync(speaker);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting speaker {SpeakerId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while deleting the speaker",
                    Details = ex.Message
                });
            }
        }
    }
}
