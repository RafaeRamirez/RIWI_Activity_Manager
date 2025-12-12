using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyQuestionController : ControllerBase
    {
        private readonly ISurveyQuestionRepository _repository;
        private readonly IMapper _mapper;

        public SurveyQuestionController(ISurveyQuestionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyQuestionDto>>> GetAll()
        {
            var questions = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SurveyQuestionDto>>(questions));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyQuestionDto>> GetById(long id)
        {
            var question = await _repository.GetByIdAsync(id);
            if (question == null) return NotFound();
            return Ok(_mapper.Map<SurveyQuestionDto>(question));
        }

        [HttpPost]
        public async Task<ActionResult<SurveyQuestionDto>> Create(CreateSurveyQuestionDto dto)
        {
            var question = _mapper.Map<SurveyQuestion>(dto);
            await _repository.AddAsync(question);
            return CreatedAtAction(nameof(GetById), new { id = question.QuestionId }, _mapper.Map<SurveyQuestionDto>(question));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateSurveyQuestionDto dto)
        {
            var question = await _repository.GetByIdAsync(id);
            if (question == null) return NotFound();

            _mapper.Map(dto, question);
            await _repository.UpdateAsync(question);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var question = await _repository.GetByIdAsync(id);
            if (question == null) return NotFound();

            await _repository.DeleteAsync(question);
            return NoContent();
        }
    }
}
