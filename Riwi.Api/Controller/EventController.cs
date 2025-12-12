using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;

        public EventController(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAll()
        {
            var events = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<EventDto>>(events));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetById(long id)
        {
            var eventEntity = await _repository.GetByIdAsync(id);
            if (eventEntity == null) return NotFound();
            return Ok(_mapper.Map<EventDto>(eventEntity));
        }

        [HttpPost]
        public async Task<ActionResult<EventDto>> Create(CreateEventDto dto)
        {
            var eventEntity = _mapper.Map<Event>(dto);
            await _repository.AddAsync(eventEntity);
            return CreatedAtAction(nameof(GetById), new { id = eventEntity.EventId }, _mapper.Map<EventDto>(eventEntity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateEventDto dto)
        {
            var eventEntity = await _repository.GetByIdAsync(id);
            if (eventEntity == null) return NotFound();

            _mapper.Map(dto, eventEntity);
            await _repository.UpdateAsync(eventEntity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var eventEntity = await _repository.GetByIdAsync(id);
            if (eventEntity == null) return NotFound();

            await _repository.DeleteAsync(eventEntity);
            return NoContent();
        }
    }
}
