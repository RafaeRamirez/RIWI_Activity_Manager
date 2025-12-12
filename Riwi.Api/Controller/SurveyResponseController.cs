using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyResponseController : ControllerBase
    {
        private readonly ISurveyResponseRepository _repository;
        private readonly IMapper _mapper;

        public SurveyResponseController(ISurveyResponseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyResponseDto>>> GetAll()
        {
            var responses = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SurveyResponseDto>>(responses));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyResponseDto>> GetById(long id)
        {
            var response = await _repository.GetByIdAsync(id);
            if (response == null) return NotFound();
            return Ok(_mapper.Map<SurveyResponseDto>(response));
        }

        [HttpPost]
        public async Task<ActionResult<SurveyResponseDto>> Create(CreateSurveyResponseDto dto)
        {
            var response = _mapper.Map<SurveyResponse>(dto);
            await _repository.AddAsync(response);
            return CreatedAtAction(nameof(GetById), new { id = response.ResponseId }, _mapper.Map<SurveyResponseDto>(response));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _repository.GetByIdAsync(id);
            if (response == null) return NotFound();

            await _repository.DeleteAsync(response);
            return NoContent();
        }
    }
}
