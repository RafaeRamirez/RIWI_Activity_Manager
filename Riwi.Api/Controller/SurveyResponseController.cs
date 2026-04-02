using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    /// <summary>
    /// Controller for managing survey responses
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SurveyResponseController : ControllerBase
    {
        private readonly ISurveyResponseRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SurveyResponseController> _logger;

        public SurveyResponseController(ISurveyResponseRepository repository, IMapper mapper, ILogger<SurveyResponseController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all survey responses
        /// </summary>
        /// <returns>List of all survey responses</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SurveyResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SurveyResponseDto>>> GetAll()
        {
            try
            {
                var responses = await _repository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<SurveyResponseDto>>(responses));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all survey responses");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving survey responses",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Get survey response by ID
        /// </summary>
        /// <param name="id">Response ID</param>
        /// <returns>Response details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SurveyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SurveyResponseDto>> GetById(long id)
        {
            try
            {
                var response = await _repository.GetByIdAsync(id);
                if (response == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Survey response with ID {id} not found"
                    });
                }
                return Ok(_mapper.Map<SurveyResponseDto>(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving survey response {ResponseId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving the survey response",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a new survey response
        /// </summary>
        /// <param name="dto">Response creation data</param>
        /// <returns>Created response</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SurveyResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SurveyResponseDto>> Create(CreateSurveyResponseDto dto)
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

                var response = _mapper.Map<SurveyResponse>(dto);
                await _repository.AddAsync(response);
                return CreatedAtAction(nameof(GetById), new { id = response.ResponseId }, _mapper.Map<SurveyResponseDto>(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating survey response");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while creating the survey response",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete a survey response
        /// </summary>
        /// <param name="id">Response ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var response = await _repository.GetByIdAsync(id);
                if (response == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Survey response with ID {id} not found"
                    });
                }

                await _repository.DeleteAsync(response);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting survey response {ResponseId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while deleting the survey response",
                    Details = ex.Message
                });
            }
        }
    }
}
