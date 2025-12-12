using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _repository;
        private readonly IMapper _mapper;

        public LocationController(ILocationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
        {
            var locations = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<LocationDto>>(locations));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetById(long id)
        {
            var location = await _repository.GetByIdAsync(id);
            if (location == null) return NotFound();
            return Ok(_mapper.Map<LocationDto>(location));
        }

        [HttpPost]
        public async Task<ActionResult<LocationDto>> Create(CreateLocationDto dto)
        {
            var location = _mapper.Map<Location>(dto);
            await _repository.AddAsync(location);
            return CreatedAtAction(nameof(GetById), new { id = location.LocationId }, _mapper.Map<LocationDto>(location));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateLocationDto dto)
        {
            var location = await _repository.GetByIdAsync(id);
            if (location == null) return NotFound();

            _mapper.Map(dto, location);
            await _repository.UpdateAsync(location);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var location = await _repository.GetByIdAsync(id);
            if (location == null) return NotFound();

            await _repository.DeleteAsync(location);
            return NoContent();
        }
    }
}
