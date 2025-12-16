using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    /// <summary>
    /// Controller for managing survey templates
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SurveyTemplateController : ControllerBase
    {
        private readonly ISurveyTemplateRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SurveyTemplateController> _logger;

        public SurveyTemplateController(ISurveyTemplateRepository repository, IMapper mapper, ILogger<SurveyTemplateController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all survey templates
        /// </summary>
        /// <returns>List of all survey templates</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SurveyTemplateDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SurveyTemplateDto>>> GetAll()
        {
            try
            {
                var templates = await _repository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<SurveyTemplateDto>>(templates));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all survey templates");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving survey templates",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Get survey template by ID
        /// </summary>
        /// <param name="id">Template ID</param>
        /// <returns>Template details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SurveyTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SurveyTemplateDto>> GetById(long id)
        {
            try
            {
                var template = await _repository.GetByIdAsync(id);
                if (template == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Survey template with ID {id} not found"
                    });
                }
                return Ok(_mapper.Map<SurveyTemplateDto>(template));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving survey template {TemplateId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving the survey template",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a new survey template
        /// </summary>
        /// <param name="dto">Template creation data</param>
        /// <returns>Created template</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SurveyTemplateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SurveyTemplateDto>> Create(CreateSurveyTemplateDto dto)
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

                var template = _mapper.Map<SurveyTemplate>(dto);
                await _repository.AddAsync(template);
                return CreatedAtAction(nameof(GetById), new { id = template.TemplateId }, _mapper.Map<SurveyTemplateDto>(template));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating survey template");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while creating the survey template",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Update an existing survey template
        /// </summary>
        /// <param name="id">Template ID</param>
        /// <param name="dto">Template update data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(long id, UpdateSurveyTemplateDto dto)
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

                var template = await _repository.GetByIdAsync(id);
                if (template == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Survey template with ID {id} not found"
                    });
                }

                _mapper.Map(dto, template);
                await _repository.UpdateAsync(template);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating survey template {TemplateId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while updating the survey template",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Delete a survey template
        /// </summary>
        /// <param name="id">Template ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var template = await _repository.GetByIdAsync(id);
                if (template == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 404,
                        Message = $"Survey template with ID {id} not found"
                    });
                }

                await _repository.DeleteAsync(template);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting survey template {TemplateId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while deleting the survey template",
                    Details = ex.Message
                });
            }
        }
    }
}
