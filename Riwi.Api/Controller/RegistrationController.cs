using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationRepository _repository;
        private readonly IMapper _mapper;

        public RegistrationController(IRegistrationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistrationDto>>> GetAll()
        {
            var registrations = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RegistrationDto>>(registrations));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistrationDto>> GetById(long id)
        {
            var registration = await _repository.GetByIdAsync(id);
            if (registration == null) return NotFound();
            return Ok(_mapper.Map<RegistrationDto>(registration));
        }

        [HttpPost]
        public async Task<ActionResult<RegistrationDto>> Create(CreateRegistrationDto dto)
        {
            var registration = _mapper.Map<Registration>(dto);
            await _repository.AddAsync(registration);
            return CreatedAtAction(nameof(GetById), new { id = registration.RegistrationId }, _mapper.Map<RegistrationDto>(registration));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateRegistrationDto dto)
        {
            var registration = await _repository.GetByIdAsync(id);
            if (registration == null) return NotFound();

            _mapper.Map(dto, registration);
            await _repository.UpdateAsync(registration);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var registration = await _repository.GetByIdAsync(id);
            if (registration == null) return NotFound();

            await _repository.DeleteAsync(registration);
            return NoContent();
        }
    }
}
