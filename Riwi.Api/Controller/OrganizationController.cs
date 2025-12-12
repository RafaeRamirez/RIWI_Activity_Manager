using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Interfaces;
using Riwi.Api.Models;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _repository;
        private readonly IMapper _mapper;

        public OrganizationController(IOrganizationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationDto>>> GetAll()
        {
            var organizations = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<OrganizationDto>>(organizations));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationDto>> GetById(long id)
        {
            var organization = await _repository.GetByIdAsync(id);
            if (organization == null) return NotFound();
            return Ok(_mapper.Map<OrganizationDto>(organization));
        }

        [HttpPost]
        public async Task<ActionResult<OrganizationDto>> Create(CreateOrganizationDto dto)
        {
            var organization = _mapper.Map<Organization>(dto);
            await _repository.AddAsync(organization);
            return CreatedAtAction(nameof(GetById), new { id = organization.OrgId }, _mapper.Map<OrganizationDto>(organization));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateOrganizationDto dto)
        {
            var organization = await _repository.GetByIdAsync(id);
            if (organization == null) return NotFound();

            _mapper.Map(dto, organization);
            await _repository.UpdateAsync(organization);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var organization = await _repository.GetByIdAsync(id);
            if (organization == null) return NotFound();

            await _repository.DeleteAsync(organization);
            return NoContent();
        }
    }
}
