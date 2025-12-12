using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    /// <summary>
    /// Controller for managing survey answers
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SurveyAnswerController : ControllerBase
    {
        private readonly ISurveyAnswerRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SurveyAnswerController> _logger;

        public SurveyAnswerController(ISurveyAnswerRepository repository, IMapper mapper, ILogger<SurveyAnswerController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all survey answers
        /// </summary>
        /// <returns>List of all survey answers</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SurveyAnswerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SurveyAnswerDto>>> GetAll()
        {
            try
            {
                var answers = await _repository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<SurveyAnswerDto>>(answers));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all survey answers");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while retrieving survey answers",
                    Details = ex.Message
                });
            }
        }

        /// <summary>
        /// Create a new survey answer
        /// </summary>
        /// <param name="dto">Answer creation data</param>
        /// <returns>Created answer</returns>
        /// <remarks>
        /// SurveyAnswer has a composite key (ResponseId, QuestionId), so it doesn't have a single ID for retrieval.
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(SurveyAnswerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SurveyAnswerDto>> Create(CreateSurveyAnswerDto dto)
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

                var answer = _mapper.Map<SurveyAnswer>(dto);
                await _repository.AddAsync(answer);
                return Ok(_mapper.Map<SurveyAnswerDto>(answer));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating survey answer");
                return StatusCode(500, new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "An error occurred while creating the survey answer",
                    Details = ex.Message
                });
            }
        }
    }
}
