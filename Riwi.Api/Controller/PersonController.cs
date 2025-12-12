using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
            var people = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PersonDto>>(people));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetById(long id)
        {
            var person = await _repository.GetByIdAsync(id);
            if (person == null) return NotFound();
            return Ok(_mapper.Map<PersonDto>(person));
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> Create(CreatePersonDto dto)
        {
            var person = _mapper.Map<Person>(dto);
            await _repository.AddAsync(person);
            return CreatedAtAction(nameof(GetById), new { id = person.PersonId }, _mapper.Map<PersonDto>(person));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdatePersonDto dto)
        {
            var person = await _repository.GetByIdAsync(id);
            if (person == null) return NotFound();

            _mapper.Map(dto, person);
            await _repository.UpdateAsync(person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var person = await _repository.GetByIdAsync(id);
            if (person == null) return NotFound();

            await _repository.DeleteAsync(person);
            return NoContent();
        }
    }
}
