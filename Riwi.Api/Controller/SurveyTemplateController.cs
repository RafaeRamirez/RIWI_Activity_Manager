using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyTemplateController : ControllerBase
    {
        private readonly ISurveyTemplateRepository _repository;
        private readonly IMapper _mapper;

        public SurveyTemplateController(ISurveyTemplateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyTemplateDto>>> GetAll()
        {
            var templates = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SurveyTemplateDto>>(templates));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyTemplateDto>> GetById(long id)
        {
            var template = await _repository.GetByIdAsync(id);
            if (template == null) return NotFound();
            return Ok(_mapper.Map<SurveyTemplateDto>(template));
        }

        [HttpPost]
        public async Task<ActionResult<SurveyTemplateDto>> Create(CreateSurveyTemplateDto dto)
        {
            var template = _mapper.Map<SurveyTemplate>(dto);
            await _repository.AddAsync(template);
            return CreatedAtAction(nameof(GetById), new { id = template.TemplateId }, _mapper.Map<SurveyTemplateDto>(template));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateSurveyTemplateDto dto)
        {
            var template = await _repository.GetByIdAsync(id);
            if (template == null) return NotFound();

            _mapper.Map(dto, template);
            await _repository.UpdateAsync(template);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var template = await _repository.GetByIdAsync(id);
            if (template == null) return NotFound();

            await _repository.DeleteAsync(template);
            return NoContent();
        }
    }
}
