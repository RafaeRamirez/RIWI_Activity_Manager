using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyAnswerController : ControllerBase
    {
        private readonly ISurveyAnswerRepository _repository;
        private readonly IMapper _mapper;

        public SurveyAnswerController(ISurveyAnswerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyAnswerDto>>> GetAll()
        {
            var answers = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SurveyAnswerDto>>(answers));
        }

        // SurveyAnswer has a composite key (ResponseId, QuestionId), so GetById(long id) won't work directly.
        // For now, I'll implement GetAll and Create. GetById needs composite key handling.
        // Or I can implement GetByResponseIdAndQuestionId.
        // Let's check Repository.cs implementation of GetByIdAsync. It takes long id.
        // So I cannot use generic GetByIdAsync for composite key.
        // I will omit GetById and Delete for now or implement custom method if needed.
        // Actually, let's see if I can query by predicate.
        
        [HttpPost]
        public async Task<ActionResult<SurveyAnswerDto>> Create(CreateSurveyAnswerDto dto)
        {
            var answer = _mapper.Map<SurveyAnswer>(dto);
            await _repository.AddAsync(answer);
            // Cannot return CreatedAtAction with single ID.
            return Ok(_mapper.Map<SurveyAnswerDto>(answer));
        }
    }
}
