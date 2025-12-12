using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeakerController : ControllerBase
    {
        private readonly ISpeakerRepository _repository;
        private readonly IMapper _mapper;

        public SpeakerController(ISpeakerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeakerDto>>> GetAll()
        {
            var speakers = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SpeakerDto>>(speakers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpeakerDto>> GetById(long id)
        {
            var speaker = await _repository.GetByIdAsync(id);
            if (speaker == null) return NotFound();
            return Ok(_mapper.Map<SpeakerDto>(speaker));
        }

        [HttpPost]
        public async Task<ActionResult<SpeakerDto>> Create(CreateSpeakerDto dto)
        {
            var speaker = _mapper.Map<Speaker>(dto);
            await _repository.AddAsync(speaker);
            return CreatedAtAction(nameof(GetById), new { id = speaker.SpeakerId }, _mapper.Map<SpeakerDto>(speaker));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateSpeakerDto dto)
        {
            var speaker = await _repository.GetByIdAsync(id);
            if (speaker == null) return NotFound();

            _mapper.Map(dto, speaker);
            await _repository.UpdateAsync(speaker);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var speaker = await _repository.GetByIdAsync(id);
            if (speaker == null) return NotFound();

            await _repository.DeleteAsync(speaker);
            return NoContent();
        }
    }
}
