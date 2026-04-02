using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    /// <summary>
    /// Controller for managing survey questions
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SurveyQuestionController : ControllerBase
    {
        private readonly ISurveyQuestionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SurveyQuestionController> _logger;

        public SurveyQuestionController(ISurveyQuestionRepository repository, IMapper mapper, ILogger<SurveyQuestionController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all survey questions
        /// </summary>
        /// <returns>List of all survey questions</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SurveyQuestionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SurveyQuestionDto>>> GetAll()
        {
            try
            {
                var questions = await _repository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<SurveyQuestionDto>>(questions));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all survey questions");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving survey questions",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Get survey question by ID
        /// </summary>
        /// <param name="id">Question ID</param>
        /// <returns>Question details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SurveyQuestionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SurveyQuestionDto>> GetById(long id)
        {
            try
            {
                var question = await _repository.GetByIdAsync(id);
                if (question == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Survey question with ID {id} not found"
                    });
                }
                return Ok(_mapper.Map<SurveyQuestionDto>(question));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving survey question {QuestionId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving the survey question",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a new survey question
        /// </summary>
        /// <param name="dto">Question creation data</param>
        /// <returns>Created question</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SurveyQuestionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SurveyQuestionDto>> Create(CreateSurveyQuestionDto dto)
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

                var question = _mapper.Map<SurveyQuestion>(dto);
                await _repository.AddAsync(question);
                return CreatedAtAction(nameof(GetById), new { id = question.QuestionId }, _mapper.Map<SurveyQuestionDto>(question));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating survey question");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while creating the survey question",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Update an existing survey question
        /// </summary>
        /// <param name="id">Question ID</param>
        /// <param name="dto">Question update data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(long id, UpdateSurveyQuestionDto dto)
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

                var question = await _repository.GetByIdAsync(id);
                if (question == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Survey question with ID {id} not found"
                    });
                }

                _mapper.Map(dto, question);
                await _repository.UpdateAsync(question);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating survey question {QuestionId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating the survey question",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete a survey question
        /// </summary>
        /// <param name="id">Question ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var question = await _repository.GetByIdAsync(id);
                if (question == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Survey question with ID {id} not found"
                    });
                }

                await _repository.DeleteAsync(question);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting survey question {QuestionId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while deleting the survey question",
                    Details = ex.Message
                });
            }
        }
    }
}
